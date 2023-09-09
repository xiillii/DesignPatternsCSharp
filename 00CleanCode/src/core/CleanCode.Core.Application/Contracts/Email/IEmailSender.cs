using CleanCode.Core.Application.Models.Email;

namespace CleanCode.Core.Application.Contracts.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(EmailMessage email);
}
