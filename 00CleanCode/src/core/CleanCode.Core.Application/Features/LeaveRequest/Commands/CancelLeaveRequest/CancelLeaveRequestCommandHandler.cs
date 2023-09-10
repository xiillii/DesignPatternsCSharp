using CleanCode.Core.Application.Contracts.Email;
using CleanCode.Core.Application.Contracts.Logging;
using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Exceptions;
using CleanCode.Core.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using CleanCode.Core.Application.Models.Email;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler
    : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _repository;
    private readonly IAppLogger<CancelLeaveRequestCommandHandler> _logger;
    private readonly IEmailSender _emailSender;

    public CancelLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveTypeRepository,
        IAppLogger<CancelLeaveRequestCommandHandler> logger,
        IEmailSender emailSender)
    {

        _repository = leaveTypeRepository;
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        // get object from database
        var lRequestToCancel = await _repository
            .GetByIdAsync(request.Id) ??
                throw new NotFoundException(nameof(Domain.LeaveRequest)
                    , request.Id);

        // cancel and save
        lRequestToCancel.Cancelled = true;

        // TODO: If already Approved, Re-evaluate the employee's allocations for the leave type

        await _repository.UpdateAsync(lRequestToCancel);


        // send confirmation email
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, // TODO: get email from employee record
                Body = $"Your leave request for {lRequestToCancel.StartDate:D} to {lRequestToCancel.EndDate:D} " +
                        $"has been cancelled successfully.",
                Subject = "Leave Request Cancelled",
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);

        }

        // return unit value
        return Unit.Value;
    }
}
