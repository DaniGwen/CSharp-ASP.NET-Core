using Eventures.App.Areas.Identity.Data;
using Eventures.App.Data;
using Eventures.App.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult All()
        {
            var eventsDb = this.context.Events
                .Select(e => new EventAllViewModel
                {
                    Name = e.Name,
                    Start = e.Start.ToString(),
                    End = e.End.ToString(),
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
