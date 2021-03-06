﻿using System.Diagnostics;
using System.Linq;
using Data.Context;
using Data.Models;
using FDMC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FDMC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly FDMCContext context;

        public HomeController(ILogger<HomeController> logger, FDMCContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            this.ViewData["allCats"] = this.context.Cats.ToList();

            return View();
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

        public IActionResult AddCat()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCat(AddCatModel model)
        {
            var cat = new Cat
            {
                Age = model.Age,
                Breed = model.Breed,
                ImageUrl = model.ImageUrl,
                Name = model.Name
            };

            this.context.Cats.Add(cat);
            this.context.SaveChanges();

            return Redirect("/Home");
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.Redirect("/Home");
            }

            var catFromDb = this.context.Cats.FirstOrDefault(cat => cat.Id == id);

            return View(catFromDb);
        }
    }
}
