using Microsoft.Extensions.Configuration;
using RentCargoBus.Services.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace RentCargoBus.Services
{
    public class EmailService
    {
        private readonly IConfiguration configuration;
        private readonly IGeneralService generalService;

        private string Apikey => configuration.GetValue<string>("SendGrid:rent-a-van_ApiKey");

        public EmailService(IConfiguration configuration, IGeneralService generalService)
        {
            this.configuration = configuration;
            this.generalService = generalService;
        }

        public async Task<Response> SendEmail(string senderEmail,
                                    string sender,
                                    string vehicleBrand,
                                    string vehicleModel,
                                    string plate,
                                    string senderCountry)
        {
            var emailPhoneDb = await this.generalService.GetPhoneEmail();
            var recipient = emailPhoneDb.Email;

            var apiKey = this.Apikey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(senderEmail, sender);
            var subject = "Hire a vehicle";
            var to = new EmailAddress(this.configuration.GetSection("AdminConfig:Email").Value /*recipient*/, "rentavan.azurewebsites.net");
            var plainTextContent = "";
            var htmlContent =
                $"<h4>{senderEmail} is requesting vehicle:</h4>" +
                $"<br/><p><strong>{vehicleBrand} {vehicleModel}</strong> with plate number <strong>\"{plate}\"</strong><p><br/>" +
                $"<p>Sender country: {senderCountry}</p><br/>" +
                "<small><strong>Rent-A-Van</strong><small>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(true);

            return response;
        }

        public async Task<Response> EmailChangeConfirmation(string currentEmail, string email, string body)
        {
            var apiKey = this.Apikey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(currentEmail, "Rent-A-Van");
            var subject = "Confirm your email";
            var to = new EmailAddress(email, "Rent-A-Van");
            var plainTextContent = "";
            var htmlContent = body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

            return response;
        }
    }
}
