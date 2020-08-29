using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RentAVan.Web.Models;
using RentCargoBus.Services;
using RentCargoBus.Services.Contracts;
using RentCargoBus.Web.Models.Index;
using Resources;

namespace RentCargoBus.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IVanService vanService;
        private readonly ICarService carService;
        private readonly IMapper mapper;
        private readonly EmailService emailService;
        private readonly IStringLocalizer localizer;
        private readonly IDeliveryService deliveryService;

        public HomeController(ILogger<HomeController> logger
                             , IVanService vanService
                             , ICarService carService
                             , IMapper mapper
                             , EmailService emailService
                             , IStringLocalizer<SharedResources> localizer
                             , IDeliveryService deliveryService)
        {
            this.logger = logger;
            this.vanService = vanService;
            this.carService = carService;
            this.mapper = mapper;
            this.emailService = emailService;
            this.localizer = localizer;
            this.deliveryService = deliveryService;
        }

        [AllowAnonymous]
        public IActionResult SetCultureCookie(string cltr, string returnUrl)
        {
            Response.Cookies.Append(

                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddMonths(12) /*IsEssential = true*/, Secure = true }
            );

            if (returnUrl == "///")
            {
                return this.Redirect("/Home/Index");
            }

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
            var deliveryFeesDb = this.deliveryService.GetDeliveryFees();

            var viewModel = this.mapper.Map<VansViewModel>(vanDb);

            viewModel.Delivery = deliveryFeesDb.VanDelivery;

            return this.View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CarDetails(int id)
        {
            var carImages = await this.carService.GetImagesByCarIdAsync(id);
            var carDb = await this.carService.GetCarByIdAsync(id);
            var deliveryFeesDb = this.deliveryService.GetDeliveryFees();

            var viewModel = this.mapper.Map<CarsViewModel>(carDb);

            viewModel.Delivery = deliveryFeesDb.CarDelivery;

            return this.View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendEmail(string senderEmail, string sender, string brand, string model, string plate)
        {
            if (string.IsNullOrEmpty(senderEmail) || string.IsNullOrEmpty(sender))
            {
                return this.Json(new { message = this.localizer["Please provide Name and Email"] });
            }

            var response = await this.emailService
                .SendEmail(senderEmail, sender, brand, model, plate);

            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                return this.Json(new { message = this.localizer["Request send! Thank you."] });
            }

            return this.Json(new { message = this.localizer["Could not send request...Please use the provided phone number. Thank you."] });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult PrivacyRevoke()
        {
            HttpContext.Features.Get<ITrackingConsentFeature>().WithdrawConsent();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
