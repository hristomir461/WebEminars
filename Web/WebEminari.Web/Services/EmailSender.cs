using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.UI.Services;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace WebEminari.Web.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var sendGridKey = @"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";
            return Execute(sendGridKey, subject, htmlMessage, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("hristomir.hari@gmail.com", "Fitrianingrum via SendGrid"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
