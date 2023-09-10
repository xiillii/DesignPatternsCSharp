using AutoMapper;
using CleanCode.Core.Application.Contracts.Email;
using CleanCode.Core.Application.Contracts.Logging;
using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Exceptions;
using CleanCode.Core.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using CleanCode.Core.Application.Features.LeaveType.Commands.UpdateLeaveType;
using CleanCode.Core.Application.Models.Email;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveRequest.Commands.ChangeApprovalLeaveRequest;

public class ChangeApprovalLeaveRequestCommandHandler
    : IRequestHandler<ChangeApprovalLeaveRequestCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IAppLogger<ChangeApprovalLeaveRequestCommandHandler> _logger;
    private readonly IEmailSender _emailSender;

    public ChangeApprovalLeaveRequestCommandHandler(IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveRequestRepository leaveRequestRepository,
        IAppLogger<ChangeApprovalLeaveRequestCommandHandler> logger,
        IEmailSender emailSender)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task<Unit> Handle(ChangeApprovalLeaveRequestCommand request, CancellationToken cancellationToken)
    {

        // validate request
        var validator = new ChangeApprovalLeaveRequestCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation error in change approval request for {0} - {1}",
                nameof(Domain.LeaveRequest), request.Id);
            throw new BadRequestException("Invalid LeaveRequest", validationResult);
        }

        // get item from database
        var lRequest = await _leaveRequestRepository.GetByIdAsync(request.Id) ??
            throw new NotFoundException(nameof(Domain.LeaveRequest), request.Id);


        // change the value
        lRequest.Approved = request.Approved;

        // update to database
        await _leaveRequestRepository.UpdateAsync(lRequest);

        // TODO: if request is approved, get and update the employee's allocations

        // send confirmation email
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, // TODO: get email from employee record
                Body = $"The approval status for your leave request for {lRequest.StartDate:D} to {lRequest.EndDate:D} " +
                        $"has been updated.",
                Subject = "Leave Request Approval Status Updated",
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
