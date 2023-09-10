using CleanCode.Ui.BlazorUi.Contracts;
using CleanCode.Ui.BlazorUi.Services.Base;

namespace CleanCode.Ui.BlazorUi.Services;

public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
{
    public LeaveAllocationService(IClient client) : base(client)
    {
    }
}
