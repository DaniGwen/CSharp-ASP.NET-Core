using KniveGallery.Web.Data;
using KniveGallery.Web.Models;
using KniveGallery.Web.Services.EmailService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var message = "Successful order";
            foreach (var kniveId in order.KniveIds)
            {
                var kniveOrderDto = new OrderedKniveIds
                {
                    KniveId = kniveId,
                    OrderId = order.OrderId,
                    Order = order
                };
                order.OrderedKniveIds.Add(kniveOrderDto);
            }
            order.IsDelivered = false;
            order.OrderDate = DateTime.Now.ToString("d-MM-yyyy H:mm");
            try
            {
                await this.dbContext.AddAsync(order);
                await this.dbContext.SaveChangesAsync();
                var senderName = $"{order.FirstName} {order.LastName}";

                //await this.emailService.SendEmail(order.Email, senderName, order.KniveId, order.Quantity);
            }
            catch (Exception e)
            {
                message = "Order could not be processed.";
            }
            return Json(message);
        }

        [HttpGet]
        public ActionResult<List<Order>> GetOrders()
        {
            var ordersDb = this.dbContext.Orders.Include("OrderedKniveIds").ToList();
            foreach (var order in ordersDb)
            {
                foreach (var knive in order.OrderedKniveIds)
                {
                    order.KniveIds.Add(knive.KniveId);
                }
            }
            return ordersDb;
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
