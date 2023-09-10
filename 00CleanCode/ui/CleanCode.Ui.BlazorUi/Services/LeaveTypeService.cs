using CleanCode.Ui.BlazorUi.Contracts;
using CleanCode.Ui.BlazorUi.Services.Base;

namespace CleanCode.Ui.BlazorUi.Services;

public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
    public LeaveTypeService(IClient client) : base(client)
    {
    }
}
