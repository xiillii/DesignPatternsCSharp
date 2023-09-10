using CleanCode.Core.Application.Contracts.Persistence;
using FluentValidation;

namespace CleanCode.Core.Application.Features.LeaveAllocation.Commads.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandValidator
    : AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveAllocationRepository _repository;

    public CreateLeaveAllocationCommandValidator(ILeaveAllocationRepository repository)
    {
        _repository = repository;

        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist.");
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        var leaveType = await _repository.GetByIdAsync(id);

        return leaveType != null;
    }
}
