using MediatR;

namespace CleanCode.Core.Application.Features.LeaveAllocation.Commads.CreateLeaveAllocation;

public class CreateLeaveAllocationCommand : IRequest<int>
{
    public int LeaveTypeId { get; set; }
}
