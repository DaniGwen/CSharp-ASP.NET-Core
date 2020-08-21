using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RentCargoBus.Data.Models;
using RentCargoBus.Services.Contracts;
using RentCargoBus.Web.Models.EditViewModels;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RentCargoBus.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IVanService vanService;
        private readonly ICarService carService;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment hostEnvironment;

        public AdminController(IVanService vanService
                               , ICarService carService
                               , IMapper mapper
                               ,IWebHostEnvironment hostEnvironment)
        {
            this.vanService = vanService;
            this.carService = carService;
            this.mapper = mapper;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteVan(int id)
        {
            try
            {
                await this.vanService.RemoveVanByIdAsync(id);
                return this.Json("Van removed!");
            }
            catch (System.Exception)
            {
                return this.Json("Error deleting van!", false);
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                await this.carService.RemoveCarByIdAsync(id);

                return this.Json("Car removed!");
            }
            catch (Exception)
            {
                return this.Json("Error deleting car!", false);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditVan(int id)
        {
            var vanDb = await this.vanService.GetVanById(id);
            var images = this.vanService.GetImagesById(id);

            var viewModel = this.mapper.Map<VanEditViewModel>(vanDb);
            viewModel.Images = images;

            return this.View(viewModel);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditVan([FromForm] VanEditPostModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            //TODO get van and add images
            //TODO Delete all images from Image collection

            if (viewModel.Images.Count > 0)
            {
                foreach (var image in viewModel.Images)
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
            return this.View();
        }
    }
}
