using Eventures.App.Areas.Identity.Data;
using Eventures.App.Data;
using Eventures.App.Models.BindingModels;
using Eventures.App.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Linq;

namespace Eventures.App.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventuresDbContext context;

        public EventsController(EventuresDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult Create(EventCreateBindingModel bindingModel)
        {
            if (this.ModelState.IsValid)
            {
                Event eventForDb = new Event
                {
                    Id =  Guid.NewGuid().ToString(),
                    Name = bindingModel.Name,
                    End = bindingModel.End,
                    Place = bindingModel.Place,
                    Start = bindingModel.Start,
                    PricePerTicket = bindingModel.PricePerTicket,
                    TotalTickets = bindingModel.TotalTickets
                };

                this.context.Events.Add(eventForDb);
                this.context.SaveChanges();

                return RedirectToAction("All");
            }
            return this.View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult All()
        {
            var eventsDb = this.context.Events
                .Select(e => new EventAllViewModel
                {
                    Name = e.Name,
                    Start = e.Start.ToString("dd-MMM-yyyy HH:MM", CultureInfo.InvariantCulture),
                    End = e.End.ToString("dd-MMM-yyyy HH:MM", CultureInfo.InvariantCulture),
                    Place = e.Place
                })
                .ToList();

            return this.View(eventsDb);
        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}
