using CleanCode.Core.Application.Contracts.Persistence;
using FluentValidation;

namespace CleanCode.Core.Application.Features.LeaveAllocation.Commads.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator
    : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _LeaveAllocationrepository;

    public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository repository,
        ILeaveAllocationRepository LeaveAllocationrepository)
    {
        _leaveTypeRepository = repository;
        _LeaveAllocationrepository = LeaveAllocationrepository;

        RuleFor(p => p.NumberOfDays)
                .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");

        RuleFor(p => p.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}");

        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist.");

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveAllocationMustExist)
            .WithMessage("{PropertyName} must be present");
    }

    private async Task<bool> LeaveAllocationMustExist(int id, CancellationToken token)
    {
        var lAllocation = await _LeaveAllocationrepository.GetByIdAsync(id);

        return lAllocation != null;
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        var lType = await _leaveTypeRepository.GetByIdAsync(id);

        return lType != null;
    }
}
