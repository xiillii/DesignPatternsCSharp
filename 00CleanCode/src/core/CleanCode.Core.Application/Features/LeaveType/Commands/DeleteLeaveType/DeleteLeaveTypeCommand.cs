using MediatR;

namespace CleanCode.Core.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
