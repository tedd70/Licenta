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

        public void SendEmail(string userEmail, string subject, string body)
        {
            try
            {
                var msg = new MailMessage(_fromEmail, userEmail, subject,
                body);
            msg.IsBodyHtml = true;

            
                _client.Send(msg);
            }
            catch
            { }
        }
    }

    public interface IEmailService
    {
        void SendEmail(string userEmail, string subject, string body);
    }
}
