using CleanCode.Core.Application.Contracts.Persistence;
using FluentValidation;

namespace CleanCode.Core.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidator 
    : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _repository;

    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository repository)
    {
        this._repository = repository;

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveTypeMustExist);
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70)
            .WithMessage("{PropertyName} must be fewer than 70 characters");

        RuleFor(p => p.DefaultDays)
            .LessThan(100)
            .WithMessage("{PropertyName} cannot exceed 100")
            .GreaterThan(1)
            .WithMessage("{PropertyName} cannot be less than 1");

        RuleFor(p => p)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave type already exists");

    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        var leaveType = await _repository.GetByIdAsync(id);

        return leaveType != null;   
    }

    private Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command
        , CancellationToken token)
        => _repository.IsLeaveTypeUnique(command.Name);
}
