using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

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

        public async Task SendNewPassword(string newPassword, string email, string username)
        {
            var emailSubject = "Your new password. Digital Cool Book.";

            var emailBody = $"Здравейте, {username}"
                             + "\r\n" + "\r\n" +
                             $"Вашата нова парола е: {newPassword}" +
                             "\r\n" + "Променете вашата парола от настройките на вашия профил." +
                             "\r\n" + "\r\n" + "Поздрави." + "\r\n" +
                             "<strong>Digital Cool Book</strong>";

            await this.Execute( emailSubject, emailBody, email);
        }

        public async Task SendEmailAsync(string inputEmail, string confirmYourEmail, string s)
        {
            //TODO implement email confirmation
        }
    }
}
