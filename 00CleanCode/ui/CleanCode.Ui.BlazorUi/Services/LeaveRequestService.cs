using CleanCode.Ui.BlazorUi.Contracts;
using CleanCode.Ui.BlazorUi.Services.Base;

namespace CleanCode.Ui.BlazorUi.Services;

public class LeaveRequestService : BaseHttpService, ILeaveRequestService
{
    public LeaveRequestService(IClient client) : base(client)
    {
    }
}
