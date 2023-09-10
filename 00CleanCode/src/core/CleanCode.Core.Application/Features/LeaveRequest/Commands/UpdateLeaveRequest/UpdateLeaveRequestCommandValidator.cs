using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Features.LeaveRequest.Shared;
using FluentValidation;

namespace CleanCode.Core.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandValidator
    : AbstractValidator<UpdateLeaveRequestCommand>
{
    
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public UpdateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository,
            ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
        
        Include(new BaseLeaveRequestValidator(leaveTypeRepository));

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveRequestMustExists)
            .WithMessage("{ProperyName} must be present");

    }

    private async Task<bool> LeaveRequestMustExists(int id, CancellationToken token)
    {
        var lRequest = await _leaveRequestRepository.GetByIdAsync(id);

        return lRequest != null;
    }
}
