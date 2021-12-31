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
                var to = new EmailAddress(email);
                var msg = MailHelper.CreateSingleEmail(from, to, settings.ConfirmMessage, "", settings.ConfirmMessage + " " + link);
                var response = await client.SendEmailAsync(msg);

                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failure(new List<string> { e.Message });
            }
        }
    }
}