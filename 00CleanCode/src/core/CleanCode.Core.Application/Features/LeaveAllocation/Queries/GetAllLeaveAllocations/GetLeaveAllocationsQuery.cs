using MediatR;

namespace CleanCode.Core.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;

public record GetLeaveAllocationsQuery : IRequest<List<LeaveAllocationDto>>;
