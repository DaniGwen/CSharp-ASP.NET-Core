using DigitalCoolBook.Services.Message;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace DigitalCoolBook.App.Services
{
    public class EmailSender : IEmailSend
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string SendGridKey { get; set; }
        private string SendGriduser { get; set; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            this.SendGridKey = _configuration["DigitalCoolBook:EmailSenderApiKey"];
            this.SendGriduser = _configuration["DigitalCoolBook:EmailSenderName"];
            //this.SendGriduser = _configuration.GetSection()
            return Execute(SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(email, SendGriduser),
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

        public async Task<string> SendNewPassword(string newPassword, string email)
        {
            var emailSubject = "Your new password. Digital Cool Book.";

            var emailBody = "Здравейте," + Environment.NewLine + $"Вашата нова парола е: {newPassword}" +
                             Environment.NewLine + "Променете вашата парола от настройките на вашия профил." +
                             Environment.NewLine + Environment.NewLine + "Поздрави." + Environment.NewLine +
                             "Digital Cool Book";

            await this.SendEmailAsync(email, emailSubject, emailBody);

            return "Новата ви парола ще бъде изпратена на посоченият имейл до няколко минути.";
        }
    }
}
