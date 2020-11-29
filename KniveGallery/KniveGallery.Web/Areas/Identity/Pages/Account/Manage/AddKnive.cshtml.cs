using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KniveGallery.Web.Data;
using KniveGallery.Web.Models;
using KniveGallery.Web.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KniveGallery.Web.Areas.Identity.Pages.Account
{
    [Authorize]
    public class AddKniveModel : PageModel
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment hostEnvironment;

        public AddKniveModel(ApplicationDbContext context
                           , IWebHostEnvironment hostEnvironment)
        {
            this.context = context;
            this.hostEnvironment = hostEnvironment;
        }

        [ViewData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public InputModel()
            {
                this.Images = new List<IFormFile>();
            }

            [Display(Name ="Edge width")]
            public double EdgeWidth { get; set; }

            [Display(Name = "Edge tickness")]
            public double EdgeThickness { get; set; }

            [Display(Name = "Total length")]
            [Required(ErrorMessage = "Fill out the input field")]
            public double TotalLength { get; set; }

            [Display(Name = "Edge length")]
            [Required(ErrorMessage = "Fill out the input field")]
            public double EdgeLength { get; set; }

            [Display(Name = "Handle description")]
            [Required(ErrorMessage = "Fill out the input field")]
            public string HandleDescription { get; set; }

            [Display(Name = "Edge made")]
            [Required(ErrorMessage = "Fill out the input field")]
            public string EdgeMade { get; set; }

            [Required(ErrorMessage = "Fill out the input field")]
            public double Price { get; set; }

            [Display(Name = "Knive category")]
            [Required]
            public string KniveClass { get; set; }

            [DataType(DataType.Upload)]
            public List<IFormFile> Images { get; set; }
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var knive = new Knive
                    {
                        EdgeWidth = Input.EdgeWidth,
                        EdgeThickness = Input.EdgeThickness,
                        EdgeMade = Input.EdgeMade,
                        EdgeLength = Input.EdgeLength,
                        HandleDescription = Input.HandleDescription,
                        TotalLength = Input.TotalLength,
                        Price = Input.Price,
                        KniveClass = (KniveClass)Enum.Parse(typeof(KniveClass), Input.KniveClass)
                    };

                    knive.Images.Clear();

                    if (Input.Images.Count > 0)
                    {
                        foreach (var image in Input.Images)
                        {
                            var newImage = new KniveImage();
                            string clientAppAssets = string.Empty;
                            // Save image to wwwroot / image
                            if (hostEnvironment.EnvironmentName == "Development")
                            {
                                clientAppAssets = this.hostEnvironment.ContentRootPath + "/ClientApp/src/assets";
                            }
                            else
                            {
                                clientAppAssets = this.hostEnvironment.ContentRootPath + "/ClientApp/dist/assets";
                            }

                            string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                            string extension = Path.GetExtension(image.FileName);
                            newImage.ImagePath = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                            string path = Path.Combine(clientAppAssets + "/images/", fileName);

                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await image.CopyToAsync(fileStream);
                            }

                            //Insert record
                            knive.Images.Add(newImage);
                        }
                    }

                    await this.context.Knives.AddAsync(knive);
                    await this.context.SaveChangesAsync();

                    StatusMessage = "Successfuly saved!";

                    return this.Page();
                }
                StatusMessage = "Error. Couldn't save the knive. Check all fields and try again.";
                return this.Page();
            }
            catch (Exception)
            {
                StatusMessage = "Error. Could not save the knive.";
                return this.Page();
            }
        }
    }
}
