namespace CleanCode.Core.Application.Models.Email;

public class EmailSettings
{
    public string ApiKey { get; set; }
    public string Region { get; set; }
    public string FromAddress { get; set; }
    public string FromName { get; set; }
    public string ConfigSet { get; set; }
}