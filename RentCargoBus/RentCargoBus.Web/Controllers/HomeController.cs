using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentCargoBus.Data.Models;
using RentCargoBus.Data.Models.Enum;
using RentCargoBus.Services;
using RentCargoBus.Services.Contracts;
using RentCargoBus.Web.Models;
using RentCargoBus.Web.Models.Index;

namespace RentCargoBus.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IVanService vanService;
        private readonly IMapper mapper;

        public HomeController(ILogger<HomeController> logger
                             ,IVanService vanService
                             ,IMapper mapper)
        {
            this.logger = logger;
            this.vanService = vanService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var allVans = this.vanService.GetAllVans();

            //var cargoVans = allVans.Where(van => van.Type == VanType.Cargo);
            //var passangerVans = allVans.Where(van => van.Type == VanType.Passenger);

            var images = this.vanService.GetImages();

            var vansDto = this.mapper.Map<List<VansViewModel>>(allVans);

            return View(vansDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
