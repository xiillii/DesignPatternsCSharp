using CleanCode.Core.Domain.Common;

namespace CleanCode.Core.Domain;

public class LeaveAllocation : BaseEntity
{
    public string EmployeeId { get; set; } = string.Empty;
    public int NumberOfDays { get; set; }
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }
}
