using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentCargoBus.Services;
using RentCargoBus.Services.Contracts;
using RentCargoBus.Web.Models;
using RentCargoBus.Web.Models.Index;
using SendGrid;

namespace RentCargoBus.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IVanService vanService;
        private readonly ICarService carService;
        private readonly IMapper mapper;
        private readonly EmailService emailService;

        public HomeController(ILogger<HomeController> logger
                             , IVanService vanService
                             , ICarService carService
                             , IMapper mapper
                             , EmailService emailService)
        {
            this.logger = logger;
            this.vanService = vanService;
            this.carService = carService;
            this.mapper = mapper;
            this.emailService = emailService;
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> VanDetails(int id)
        {
            var vanImages = this.vanService.GetImagesByVanId(id);
            var vanDb = await this.vanService.GetVanByIdAsync(id);

            var viewModel = this.mapper.Map<VansViewModel>(vanDb);

            return this.View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CarDetails(int id)
        {
            var carImages = await this.carService.GetImagesByCarIdAsync(id);
            var carDb = await this.carService.GetCarByIdAsync(id);

            var viewModel = this.mapper.Map<CarsViewModel>(carDb);

            return this.View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task SendEmail()
        {

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendEmail(string senderEmail, string sender, string brand, string model, string plate)
        {

            var response = await this.emailService
                .SendEmail(senderEmail, sender, brand, model, plate);

            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                return this.Json(true);
            }

            return this.Json(false);
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
