using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using CleanCode.Core.Application.Contracts.Email;
using CleanCode.Core.Application.Models.Email;
using Microsoft.Extensions.Options;

namespace CleanCode.Infrastructure.EmailService.Aws;

public class EmailSender : IEmailSender
{
    public EmailSettings EmailSettings { get; }

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        EmailSettings = emailSettings.Value;
    }

    public async Task<bool> SendEmail(EmailMessage email)
    {

        RegionEndpoint region = GetAwsRegion(EmailSettings.Region);

        using (var client = new AmazonSimpleEmailServiceClient(region))
        {
            var sendRequest = new SendEmailRequest
            {
                Source = $"{EmailSettings.FromName}<{EmailSettings.FromAddress}>",
                Destination = new Destination
                {
                    ToAddresses = new List<string> { email.To }
                },
                Message = new Message
                {
                    Subject = new Content(email.Subject),
                    Body = new Body
                    {
                        Html = new Content
                        {
                            Charset = "UTF-8",
                            Data = email.Body
                        },
                        Text = new Content
                        {
                            Charset = "UTF-8",
                            Data = email.Body
                        },
                    }
                },
                ConfigurationSetName = EmailSettings.ConfigSet
            };


            var response = await client.SendEmailAsync(sendRequest);

            return response != null
                && (response.HttpStatusCode == System.Net.HttpStatusCode.OK
                    || response.HttpStatusCode == System.Net.HttpStatusCode.Accepted);
        }
    }

    private RegionEndpoint GetAwsRegion(string region)
        => RegionEndpoint.GetBySystemName(region);
}
