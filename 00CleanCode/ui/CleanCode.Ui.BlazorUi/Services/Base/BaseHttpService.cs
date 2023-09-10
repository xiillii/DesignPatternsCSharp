namespace CleanCode.Ui.BlazorUi.Services.Base;

public class BaseHttpService
{
    private readonly IClient _client;

    public BaseHttpService(IClient client)
    {
        _client = client;
    }
}