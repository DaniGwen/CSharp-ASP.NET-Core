using KniveGallery.Web.Models;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace KniveGallery.Web.Services.EmailService
{

    public class EmailService
    {
        private readonly IConfiguration configuration;

        private string Apikey => configuration.GetValue<string>("SendGrid:KniveGallery_ApiKey");

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<Response> SendEmail(string senderEmail,
                                    string sender,
                                    int kniveId,
                                    int quantity)
        {
            var apiKey = this.Apikey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(senderEmail, sender);
            var subject = "Поръчка на нож";
            var to = new EmailAddress(this.configuration.GetSection("AdminConfig:Email").Value /*recipient*/, "kirilcustomknives.azurewebsites.net");
            var plainTextContent = "";
            var htmlContent =
                $"<h4>{senderEmail} поръча {quantity} нож/ножа:</h4>" +
                $"<br/><p><strong>Номер на ножа {kniveId}</strong><p><br/>" +
                "<small><strong>kiril-custom-knives</strong><small>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg)/*.ConfigureAwait(true)*/;

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

        public async Task<Response> SendConfirmationToBuyer(Order order)
        {
            var apiKey = this.Apikey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("kiril-custom-knives");
            var subject = "Поръчка в kiril-custom-knives";
            var to = new EmailAddress(order.Email);
            var plainTextContent = "";
            var htmlContent =
                $"<h3>Вашата поръчка беше изпратена на посоченият адрес.</h3>" +
                $"<br/><h5>Вашият адрес:</h5>" +
                $"<br/><p>Град: {order.City}</p>" +
                $"<br/><p>Квартал: {order.Neighbourhood}</p>" +
                $"<br/><p>Улица: {order.Street}</p>" +
                $"<br/><p><strong></strong><p><br/>" +
                "<small><strong>kiril-custom-knives</strong><small>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg)/*.ConfigureAwait(true)*/;

            return response;
        }
    }
}
