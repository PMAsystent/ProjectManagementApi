using MimeKit;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.Base.Model;
using System;
using Infrastructure.Identity.Helpers;
using MailKit.Net.Smtp;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    static class EmailService
    {
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
