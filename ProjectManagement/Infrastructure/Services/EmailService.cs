using ProjectManagement.Core.Base.Model;
using System;
using Infrastructure.Identity.Helpers;
using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    static class EmailService
    {
        static public async Task<Result> SendEmail(string email, string link, EmailProviderSettings settings)
        {
            try
            {
                var client = new SendGridClient(settings.SendGridApiKey);
                var from = new EmailAddress(settings.SenderEmail, "PMAsystent");
                var to = new EmailAddress(email, "PMAsystent");
                var msg = MailHelper.CreateSingleEmail(from, to, settings.ConfirmMessage, "PMAsystent", settings.ConfirmMessage + " " + link);
                var response = await client.SendEmailAsync(msg);
                if(response.IsSuccessStatusCode)
                {
                    return Result.Success();
                }
                return Result.Failure(new List<string> { "Email service error: " + response.StatusCode.ToString() });

            }
            catch (Exception e)
            {
                return Result.Failure(new List<string> { e.Message });
            }
        }
    }
}