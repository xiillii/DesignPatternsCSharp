using CleanCode.Ui.BlazorUi.Models.LeaveTypes;
using CleanCode.Ui.BlazorUi.Services.Base;

namespace CleanCode.Ui.BlazorUi.Contracts;

public interface ILeaveTypeService
{
    Task<List<LeaveTypeVM>> GetLeaveTypes();
    Task<LeaveTypeVM> GetLeaveTypeDetails(int id);
    Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType);
    Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeVM leaveType);
    Task<Response<Guid>> DeleteLeaveType(int id);
}
