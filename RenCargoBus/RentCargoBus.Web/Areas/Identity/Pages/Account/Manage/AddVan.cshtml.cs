using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentCargoBus.Data.Models;
using RentCargoBus.Services.Contracts;

namespace RentCargoBus.Web.Areas.Identity.Pages.Account.Manage
{
    public class AddVanModel : PageModel
    {
        private readonly IVanService vanService;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment hostEnvironment;

        public AddVanModel(IVanService vanService
                           , IMapper mapper
                           , IWebHostEnvironment hostEnvironment)
        {
            this.vanService = vanService;
            this.mapper = mapper;
            this.hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public InputModel Model { get; set; }

        public class InputModel
        {
            public InputModel()
            {
                this.Images = new List<IFormFile>();
            }

            [Required]
            [DisplayName("Brand")]
            public string Brand { get; set; }

            [Required]
            [DisplayName("Model")]
            public string Model { get; set; }

            [DisplayName("Plate Number")]
            public string PlateNumber { get; set; }

            public int Seats { get; set; }

            public double Weight { get; set; }

            public double Height { get; set; }

            [DisplayName("Max Load")]
            public int MaxLoad { get; set; }

            [DisplayName("Consumption")]
            public double KilometersPerLiter { get; set; }

            [Display(Name = "Warranty Deposit for Bulgaria")]
            public decimal Deposit { get; set; }

            [Display(Name = "Warranty Deposit for Europe")]
            public decimal DepositEu { get; set; }

            [DisplayName("Price per Day")]
            public decimal HirePrice { get; set; }

            [DisplayName("Price per Month")]
            public decimal HirePriceMonth { get; set; }

            public int Type { get; set; }

            [DataType(DataType.Upload)]
            public List<IFormFile> Images { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (this.ModelState.IsValid)
            {
                var van = this.mapper.Map<Van>(this.Model);
                van.Images.Clear();

                if (Model.Images.Count > 0)
                {
                    foreach (var image in Model.Images)
                    {
                        var newImage = new VanImage();

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
                        van.Images.Add(newImage);
                    }
                }

                await this.vanService.AddVanAsync(van);

                return Redirect("/Identity/Account/Manage");
            }

            return this.Page();
        }
    }
}
