using CleanCode.Core.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace CleanCode.Core.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public class LeaveRequestDto
{
    public string RequestingEmployeeId { get; set; } = string.Empty;
    public LeaveTypeDto? LeaveType { get; set; }
    public DateTime DateRequested { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool? Approved { get; set; }
}
