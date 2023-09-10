using AutoMapper;
using CleanCode.Core.Application.Contracts.Email;
using CleanCode.Core.Application.Contracts.Logging;
using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Exceptions;
using CleanCode.Core.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using CleanCode.Core.Application.Features.LeaveType.Commands.CreateLeaveType;
using CleanCode.Core.Application.Models.Email;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler
    : IRequestHandler<CreateLeaveRequestCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;
    private readonly IEmailSender _emailSender;

    public CreateLeaveRequestCommandHandler(IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveRequestRepository leaveRequestRepository,
        IAppLogger<CreateLeaveRequestCommandHandler> logger,
        IEmailSender emailSender)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        // validate incoming data
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation error in create request for {0}",
                nameof(Domain.LeaveRequest));
            throw new BadRequestException("Invalid LeaveRequest", validationResult);
        }

        // TODO: Get requesting employee's id
        // TODO: check on employee's allocation
        // TODO: if allocation aren't enought, return validation error with message

        // convert to domain entity object
        var lRequest = _mapper.Map<Domain.LeaveRequest>(request);

        // add to database
        await _leaveRequestRepository.CreateAsync(lRequest);

        // send confirmation email
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, // TODO: get email from employee record
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                        $"has been submitted successfully.",
                Subject = "Leave Request Submitted",
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            
        }

        // return record id
        return lRequest.Id;
    }
}
