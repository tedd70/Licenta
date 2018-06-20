using Ads.Database;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Ads.Services
{
    public class EmailService : IEmailService
    {
        private SmtpClient _client;
        private static string _fromEmail = "tedi.licenta@gmail.com";
        public EmailService()
        {
            _client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(_fromEmail, "tedilicenta"),
                EnableSsl = true
            };
        }

        public void SendResetLinkEmail(string userEmail, string resetLink)
        {
             var msg = new MailMessage(_fromEmail, userEmail, "Reset your password",
                $"Click <a href='{resetLink}' target='_blank'>here</a> to reset your password.");
            msg.IsBodyHtml = true;

            _client.Send(msg);
        }
    }

    public interface IEmailService
    {
        void SendResetLinkEmail(string userEmail, string resetLink);
    }
}
