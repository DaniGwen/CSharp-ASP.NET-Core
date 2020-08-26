using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RentAVan.Web.Models;
using RentCargoBus.Services;
using RentCargoBus.Services.Contracts;
using RentCargoBus.Web.Models.Index;

namespace RentCargoBus.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IVanService vanService;
        private readonly ICarService carService;
        private readonly IMapper mapper;
        private readonly EmailService emailService;
        private readonly IHtmlLocalizer localizer;

        public HomeController(ILogger<HomeController> logger
                             , IVanService vanService
                             , ICarService carService
                             , IMapper mapper
                             , EmailService emailService
                             , IHtmlLocalizer<Resources.SharedResources> localizer)
        {
            this.logger = logger;
            this.vanService = vanService;
            this.carService = carService;
            this.mapper = mapper;
            this.emailService = emailService;
            this.localizer = localizer;
        }

        public IActionResult SetCultureCookie(string cltr, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddMonths(3) }
            );

            return this.Redirect(returnUrl);
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendEmail(string senderEmail, string sender, string brand, string model, string plate)
        {
            if (string.IsNullOrEmpty(senderEmail) || string.IsNullOrEmpty(sender))
            {
                return this.Json(new { message = this.localizer["Please provide Name and Email.."] });
            }

            var response = await this.emailService
                .SendEmail(senderEmail, sender, brand, model, plate);

            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                return this.Json(new { message = this.localizer["<h4>Request send!</h4><br/><h5>Thank you.</h5>"] });
            }

            return this.Json(new { message = this.localizer["<strong>Could not send request...</strong><br/><p>Please use the provided phone number.<br/>Thank you.</p>"] });
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
