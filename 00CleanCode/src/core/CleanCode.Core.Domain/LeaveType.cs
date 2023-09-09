using CleanCode.Core.Domain.Common;

namespace CleanCode.Core.Domain;

public class LeaveType : BaseEntity
{    
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}
