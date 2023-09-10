using MediatR;

namespace CleanCode.Core.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public record GetLeaveRequestDetailsQuery(int Id) : IRequest<LeaveRequestDetailsDto>;

