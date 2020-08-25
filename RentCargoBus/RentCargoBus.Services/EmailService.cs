using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace RentCargoBus.Services
{
    public class EmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<Response> SendEmail(string senderEmail,
                                    string sender,
                                    string vehicleBrand,
                                    string vehicleModel,
                                    string plate)
        {
            //var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var apiKey = configuration.GetValue<string>("SendGrid:rent-a-van_ApiKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(senderEmail, sender);
            var subject = "Hire a vehicle";
            var to = new EmailAddress("drug_boy@abv.bg", "Rent-A-Van");
            var plainTextContent = "";
            var htmlContent = 
                $"<h4>Request for:</h4>" +
                $"<br/><p><strong>{vehicleBrand} {vehicleModel}</strong> with plate number <strong>\"{plate}\"</strong><p><br/>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

            return response;
        }
    }
}
