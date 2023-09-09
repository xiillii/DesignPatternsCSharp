using MediatR;

namespace CleanCode.Core.Application.Features.LeaveType.Commands.CreateLeaveType;

public class UpdateLeaveTypeCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}
