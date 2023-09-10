using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Exceptions;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveAllocation.Commads.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler
    : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _repository;

    public DeleteLeaveAllocationCommandHandler(
        ILeaveAllocationRepository leaveTypeRepository)
    {

        _repository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        // get the object from database
        var lAllocationToDelete = await _repository
            .GetByIdAsync(request.Id) ??
                throw new NotFoundException(nameof(Domain.LeaveAllocation), request.Id);

        // delete from database
        await _repository.UpdateAsync(lAllocationToDelete);

        // return
        return Unit.Value;
    }
}
