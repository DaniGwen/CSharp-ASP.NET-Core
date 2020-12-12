using KniveGallery.Web.Data;
using KniveGallery.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KniveGallery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment hostEnvironment;

        public ImagesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            this.context = context;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public List<CarouselImage> GetCarouselImages()
        {
            return this.context.CarouselImages.ToList();
        }

        [Authorize]
        [HttpPost]
        [Route("DeleteImage/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var imageDb = this.context.CarouselImages.FirstOrDefault(x => x.ImageId == imageId);

            if (imageDb == null)
            {
                return BadRequest("Error deleting knive.");
            }

            this.context.CarouselImages.Remove(imageDb);
            await this.context.SaveChangesAsync();

            return Ok("Image deleted!");
        }

        [Authorize]
        [HttpPost]
        [Route("AddCarouselImage")]
        public async Task<IActionResult> AddImage()
        {
            var image = this.Request.Form.Files[0];

            bool isSucceeded = await this.ProcessImage(image);

            if (!isSucceeded)
            {
                return BadRequest("Something went wrong!");
            }

            return Ok("Image added!");
        }

        private async Task<bool> ProcessImage(IFormFile image)
        {
            try
            {
                var newImage = new CarouselImage();
                string clientAppAssets = string.Empty;

                // Save image to wwwroot / image
                if (hostEnvironment.EnvironmentName == "Development")
                {
                    clientAppAssets = this.hostEnvironment.ContentRootPath + "/ClientApp/src/assets/images/";
                }
                else
                {
                    clientAppAssets = this.hostEnvironment.ContentRootPath + "/ClientApp/dist/assets/images/";
                }

                string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                string extension = Path.GetExtension(image.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(clientAppAssets, fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                newImage.ImagePath = fileName;

                this.context.CarouselImages.Add(newImage);
                await this.context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
