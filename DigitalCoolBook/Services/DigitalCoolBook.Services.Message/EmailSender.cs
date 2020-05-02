using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DigitalCoolBook.App.Services
{
    public class EmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Execute(string emailSubject, string emailBody, string email)
        {
            var SendGriduser = _configuration["DigitalCoolBook:EmailSenderName"];
            var apiKey = _configuration["DigitalCoolBook:EmailSenderApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("drug_boy@abv.bg", SendGriduser);
            var subject = emailSubject;
            var to = new EmailAddress(email, "Example User");
            var plainTextContent = emailBody;
            var htmlContent = emailBody;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            var response = await client.SendEmailAsync(msg);
        }

        public async Task<string> SendNewPassword(string newPassword, string email, string username)
        {
            var emailSubject = "Your new password. Digital Cool Book.";

            var emailBody = $"Здравейте, {username}"
                             + Environment.NewLine + Environment.NewLine +
                             $"Вашата нова парола е: {newPassword}" +
                             Environment.NewLine + "Променете вашата парола от настройките на вашия профил." +
                             Environment.NewLine + Environment.NewLine + "Поздрави." + Environment.NewLine +
                             "<strong>Digital Cool Book</strong>";

            await this.Execute( emailSubject, emailBody, email);

            return "Новата ви парола ще бъде изпратена на посоченият имейл до няколко минути.";
        }
    }
}
