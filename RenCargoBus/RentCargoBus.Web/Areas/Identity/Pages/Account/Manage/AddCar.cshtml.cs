using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using RentCargoBus.Data.Models;
using RentCargoBus.Services.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace RentCargoBus.Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class AddCarModel : PageModel
    {
        private readonly ICarService carService;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment hostEnvironment;

        public AddCarModel(ICarService carService
                           , IMapper mapper
                           , IWebHostEnvironment hostEnvironment)
        {
            this.carService = carService;
            this.mapper = mapper;
            this.hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public InputModel()
            {
                this.Images = new List<IFormFile>();
            }

            [Required]
            [Display(Name = "Brand")]
            public string Brand { get; set; }

            [Required]
            [Display(Name = "Model")]
            public string Model { get; set; }

            [Required]
            [Display(Name = "Plate Number")]
            public string PlateNumber { get; set; }

            public int Weight { get; set; }

            [Required]
            [Display(Name = "Miles per gallon")]
            public double MilesPerGallon { get; set; }

            [Required]
            [Display(Name = "Number of doors")]
            public int Doors { get; set; }

            [Display(Name = "Number of seats")]
            public int Seats { get; set; }

            [Required]
            [Display(Name = "Price for a day")]
            public decimal HirePrice { get; set; }

            [DataType(DataType.Upload)]
            public List<IFormFile> Images { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var car = this.mapper.Map<Car>(this.Input);
                car.Images.Clear();

                if (Input.Images.Count > 0)
                {
                    foreach (var image in Input.Images)
                    {
                        var newImage = new CarImage();

                        // Save image to wwwroot / image
                        string wwwRootPath = this.hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                        string extension = Path.GetExtension(image.FileName);
                        newImage.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        //Insert record
                        car.Images.Add(newImage);
                    }
                }

                await this.carService.AddCarAsync(car);
            }

            return this.Redirect("/Identity/Account/Manage");
        }
    }
}
