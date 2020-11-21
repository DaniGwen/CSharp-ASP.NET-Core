using KniveGallery.Web.Data;
using KniveGallery.Web.Models;
using KniveGallery.Web.Services.EmailService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KniveGallery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly EmailService emailService;

        public OrdersController(ApplicationDbContext dbContext, EmailService emailService)
        {
            this.dbContext = dbContext;
            this.emailService = emailService;
        }

        [HttpPost]
        [Route("AddOrder")]
        public async Task<IActionResult> AddOrder(Order order)
        {
            var message = string.Empty;
            order.IsDelivered = false;
            try
            {
                await this.dbContext.AddAsync(order);

                message = "Order has been successful";

                var senderName = $"{order.FirstName} {order.LastName}";

                await this.emailService.SendEmail(order.Email, senderName, order.KniveId, order.Quantity);
            }
            catch (Exception)
            {
                message = "Order could not be processed.";
            }

            return Ok(message);
        }
    }
}
