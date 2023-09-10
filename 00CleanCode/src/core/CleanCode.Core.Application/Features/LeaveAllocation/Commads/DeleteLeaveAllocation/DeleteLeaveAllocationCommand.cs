using MediatR;

namespace CleanCode.Core.Application.Features.LeaveAllocation.Commads.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
