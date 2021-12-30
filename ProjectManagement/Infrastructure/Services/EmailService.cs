using MimeKit;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.Base.Model;
using System;
using Infrastructure.Identity.Helpers;
using MailKit.Net.Smtp;
using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    static class EmailService
    {
        static public async Task<Result> SendEmailGrid(string email, string subject, string link, EmailProviderSettings settings)
        {
            try
            {
                var client = new SendGridClient("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");
                var from = new EmailAddress("projectmanagementapi@gmail.com");
                var to = new EmailAddress(email);
                var plainTextContent = link;
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);

                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failure(new List<string> { e.Message });
            }
        }

        static public Result SendEmail(string email, string subject, string link, EmailProviderSettings settings)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Project Management", settings.SenderEmail));

                message.To.Add(new MailboxAddress("User name", email));

                message.Subject = subject;

                message.Body = new TextPart("plain")
                {
                    Text = link,
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(settings.SenderServer, 587, false);
                    client.Authenticate(settings.SenderEmail, settings.Password);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return Result.Success();
            }
            catch(Exception e)
            {
                return Result.Failure(new List<string> { e.Message });
            }
        }
    }
}
