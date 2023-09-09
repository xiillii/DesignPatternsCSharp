using CleanCode.Core.Application.Contracts.Persistence;
using FluentValidation;

namespace CleanCode.Core.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator
    : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _repository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository repository)
    {
        this._repository = repository;

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

    private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command
        , CancellationToken token) 
        => _repository.IsLeaveTypeUnique(command.Name);
}
