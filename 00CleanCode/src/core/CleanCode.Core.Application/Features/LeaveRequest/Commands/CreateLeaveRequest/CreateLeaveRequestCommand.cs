using CleanCode.Core.Application.Features.LeaveRequest.Shared;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommand : BaseLeaveRequest, IRequest<int>
{
    public string RequestComments { get; set; } = string.Empty;
}
