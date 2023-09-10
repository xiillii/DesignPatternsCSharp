using MediatR;

namespace CleanCode.Core.Application.Features.LeaveRequest.Commands.ChangeApprovalLeaveRequest;

public class ChangeApprovalLeaveRequestCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public bool Approved { get; set; }
}
