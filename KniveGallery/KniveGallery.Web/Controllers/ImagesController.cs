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
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImagesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public List<CarouselImage> GetCarouselImages()
        {
            var carouselImages = _context.CarouselImages.ToList();
            return carouselImages;
        }

        [Authorize]
        [HttpPost]
        [Route("DeleteImage/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var imageDb = _context.CarouselImages.FirstOrDefault(x => x.ImageId == imageId);

            if (imageDb == null)
            {
                return Json("Error deleting knive");
            }

            _context.CarouselImages.Remove(imageDb);
            await _context.SaveChangesAsync();

            return Json("Image deleted");
        }

        [Authorize]
        [HttpPost]
        [Route("AddCarouselImage")]
        public async Task<IActionResult> AddImage()
        {
            var image = Request.Form.Files[0];

            bool isSucceeded = await ProcessImage(image);

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
                if (_hostEnvironment.EnvironmentName == "Development")
                {
                    clientAppAssets = _hostEnvironment.ContentRootPath + "/ClientApp/src/assets/images/";
                }
                else
                {
                    clientAppAssets = _hostEnvironment.ContentRootPath + "/ClientApp/dist/assets/images/";
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

                _context.CarouselImages.Add(newImage);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
