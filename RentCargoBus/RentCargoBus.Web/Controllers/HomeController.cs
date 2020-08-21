using System.Collections.Generic;
using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentCargoBus.Services.Contracts;
using RentCargoBus.Web.Models;
using RentCargoBus.Web.Models.Index;

namespace RentCargoBus.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IVanService vanService;
        private readonly ICarService carService;
        private readonly IMapper mapper;

        public HomeController(ILogger<HomeController> logger
                             , IVanService vanService
                             , ICarService carService
                             , IMapper mapper)
        {
            this.logger = logger;
            this.vanService = vanService;
            this.carService = carService;
            this.mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var allVans = this.vanService.GetAllVans();
            var allCars = this.carService.GetAllCars();

            var carImages = this.carService.GetAllImages();
            var vanImages = this.vanService.GetAllImages();

            var vansDto = this.mapper.Map<List<VansViewModel>>(allVans);
            var carsDto = this.mapper.Map<List<CarsViewModel>>(allCars);

            var viewModel = new CarsVansViewModel
            {
                Cars = carsDto,
                Vans = vansDto,
            };

            return View(viewModel);
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
