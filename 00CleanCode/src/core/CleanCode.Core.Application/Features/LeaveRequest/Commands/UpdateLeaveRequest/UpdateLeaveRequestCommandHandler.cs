using AutoMapper;
using CleanCode.Core.Application.Contracts.Email;
using CleanCode.Core.Application.Contracts.Logging;
using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Exceptions;
using CleanCode.Core.Application.Features.LeaveType.Commands.UpdateLeaveType;
using CleanCode.Core.Application.Models.Email;
using FluentValidation;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler
    : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _logger;
    private readonly IEmailSender _emailSender;

    public UpdateLeaveRequestCommandHandler(IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveRequestRepository leaveRequestRepository,
        IAppLogger<UpdateLeaveRequestCommandHandler> logger,
        IEmailSender emailSender)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id) ?? 
            throw new NotFoundException(nameof(Domain.LeaveRequest), request.Id);

        // validate incoming data
        var validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository, _leaveRequestRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation error in update request for {0} - {1}",
                nameof(Domain.LeaveRequest), request.Id);
            throw new BadRequestException("Invalid LeaveRequest", validationResult);
        }


        // convert to domain entity object
        _mapper.Map(request, leaveRequest);


        // add to database
        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        // send confirmation email
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, // TODO: get email from employee record
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                        $"has been update successfully.",
                Subject = "Leave Request Updated",
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            
        }


        // return Unit value
        return Unit.Value;
    }
}
