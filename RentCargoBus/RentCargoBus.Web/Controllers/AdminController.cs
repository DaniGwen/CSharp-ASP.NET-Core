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
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
                               , IWebHostEnvironment hostEnvironment)
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
            var vanDb = await this.vanService.GetVanByIdAsync(id);
            var images = this.vanService.GetImagesByVanId(id);

            var viewModel = this.mapper.Map<VanEditViewModel>(vanDb);
            viewModel.Images = images;

            return this.View(viewModel);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditVan([FromForm] VanEditPostModel viewModel)
        {
            var vanDb = await this.vanService.GetVanByIdAsync(viewModel.VanId);

            this.mapper.Map(viewModel, vanDb);

            var nullImages = vanDb.Images
               .Where(i => string.IsNullOrEmpty(i.ImageName))
               .ToList();

            foreach (var image in nullImages)
            {
                vanDb.Images.Remove(image);
            }

            if (viewModel.Images.Count > 0)
            {
                foreach (var image in viewModel.Images)
                {
                    var newImage = new VanImage();

                    // Save image to wwwroot / image and create new file name
                    string wwwRootPath = this.hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    string extension = Path.GetExtension(image.FileName);
                    newImage.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    vanDb.Images.Add(newImage);
                }
            }

            if (viewModel.Image.Count > 0)
            {
                await this.vanService.RemoveVanImagesById(viewModel.Image);
            }

            await this.vanService.SaveChangesAsync();

            return this.Redirect("/Home/Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditCar(int id)
        {
            var carImages = await this.carService.GetImagesByCarIdAsync(id);
            var carDb = await this.carService.GetCarByIdAsync(id);

            var viewModel = this.mapper.Map<CarEditViewModel>(carDb);
            viewModel.Images = carImages;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditCar(CarPostViewModel viewModel)
        {
            if (viewModel.ImagesToDelete.Count > 0)
            {
                await this.carService.RemoveCarImagesByIdAsync(viewModel.ImagesToDelete);
            }

            var carDb = await this.carService.GetCarByIdAsync(viewModel.CarId);

            this.mapper.Map(viewModel, carDb);

            var nullImages = carDb.Images
                .Where(i => string.IsNullOrEmpty(i.ImageName))
                .ToList();

            foreach (var image in nullImages)
            {
                carDb.Images.Remove(image);
            }

            if (viewModel.Images.Count > 0)
            {
                foreach (var image in viewModel.Images)
                {
                    var newImage = new CarImage();

                    // Save image to wwwroot / image and create new file name
                    string wwwRootPath = this.hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    string extension = Path.GetExtension(image.FileName);
                    newImage.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    carDb.Images.Add(newImage);
                }
            }

            await this.carService.SaveChangesAsync();

            return this.Redirect("/Home/Index");
        }
    }
}
