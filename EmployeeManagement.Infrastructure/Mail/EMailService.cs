using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Application.Contracts.Infrastructure;
using EmployeeManagement.Application.Models.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmployeeManagement.Infrastructure.Mail
{
    public class EMailService : IEmailService
    {
        public EmailSettings EmailSettings { get; }
        public ILogger<EMailService> Logger { get; }

        public EMailService(IOptions<EmailSettings> emailSettings, ILogger<EMailService> logger)
        {
            EmailSettings = emailSettings.Value;
            Logger = logger;
        }

        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(EmailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = EmailSettings.FromAddress,
                Name = EmailSettings.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);

            Logger.LogInformation("Email sent");

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            Logger.LogError("Email sending failed");

            return false;
        }

    }
}
