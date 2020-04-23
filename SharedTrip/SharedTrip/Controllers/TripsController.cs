using SIS.MvcFramework;
using SIS.MvcFramework.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        public IActionResult All()
        {
            return this.View();
        }
    }
}
