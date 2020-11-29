using KniveGallery.Web.Data;
using KniveGallery.Web.Models;
using KniveGallery.Web.Services.EmailService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            var message = "Successful order.";
            order.IsDelivered = false;
            order.OrderDate = DateTime.Now.ToString("d-MM-yyyy H:mm");

            try
            {
                await this.dbContext.AddAsync(order);
                await this.dbContext.SaveChangesAsync();
                var senderName = $"{order.FirstName} {order.LastName}";

                //await this.emailService.SendEmail(order.Email, senderName, order.KniveId, order.Quantity);
            }
            catch (Exception)
            {
                message = "Order could not be processed.";
            }

            return new JsonResult(message);
        }

        [HttpGet]
        public ActionResult<List<Order>> GetOrders()
        {
            return this.dbContext.Orders.ToList();
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                var orderDb = this.dbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
                this.dbContext.Orders.Remove(orderDb);
                await this.dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Could not delete order.");
            }


            return new JsonResult("Order has been deleted");
        }

        [HttpPost]
        [Route("DispatchOrder/{orderId}")]
        public async Task<IActionResult> DispatchOrder(int orderId)
        {
            try
            {
                var orderDb = this.dbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);

                if (orderDb != null)
                {
                    orderDb.DispatchDate = DateTime.Now.ToString("d-MM-yyyy H:mm");
                    orderDb.IsDelivered = true;
                    await this.dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return new JsonResult("Could not confirm order.");
            }

            return Ok("Order is confirmed send.");
        }
    }
}
