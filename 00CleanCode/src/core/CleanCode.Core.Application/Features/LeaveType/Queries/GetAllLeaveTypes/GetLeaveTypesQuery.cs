using MediatR;

namespace CleanCode.Core.Application.Features.LeaveType.Queries.GetAllLeaveTypes;


public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;
