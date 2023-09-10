using FluentValidation;

namespace CleanCode.Core.Application.Features.LeaveRequest.Commands.ChangeApprovalLeaveRequest;

public class ChangeApprovalLeaveRequestCommandValidator
    : AbstractValidator<ChangeApprovalLeaveRequestCommand>
{
    public ChangeApprovalLeaveRequestCommandValidator()
    {
        RuleFor(p => p.Approved)
            .NotNull()
            .WithMessage("Approval status cannot be null");
    }
}
