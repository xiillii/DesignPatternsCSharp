using MediatR;

namespace CleanCode.Core.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public record GetLeaveRequestsQuery : IRequest<List<LeaveRequestDto>>;
