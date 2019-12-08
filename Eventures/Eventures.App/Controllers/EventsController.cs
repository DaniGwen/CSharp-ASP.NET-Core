using Eventures.App.Data;
using Microsoft.AspNetCore.Mvc;

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

        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}
