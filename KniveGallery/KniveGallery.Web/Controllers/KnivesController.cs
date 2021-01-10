using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KniveGallery.Web.Data;
using KniveGallery.Web.DTOs;
using KniveGallery.Web.Models;
using KniveGallery.Web.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KniveGallery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnivesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment hostEnvironment;

        public KnivesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            this.context = context;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Knive>>> GetKnives()
        {
            var knives = await context.Knives.ToArrayAsync();
            var images = await this.context.Images.ToListAsync();

            foreach (var knive in knives)
            {
                var image = images.FirstOrDefault(i => i.KniveId == knive.KniveId);

                if (image != null)
                {
                    knive.ImagePath = image.ImagePath;
                }
            }

            return Ok(knives);
        }

        [Route("KniveClass/{kniveClass}")]
        [HttpGet]
        public async Task<IActionResult> GetKnivesByClass(string kniveClass)
        {
            var kniveType = (KniveClass)Enum.Parse(typeof(KniveClass), kniveClass);
            var images = await this.context.Images.ToListAsync();

            var knives = this.context.Knives
                .Where(knive => knive.KniveClass == kniveType)
                .ToList();

            foreach (var knive in knives)
            {
                var image = images.FirstOrDefault(i => i.KniveId == knive.KniveId);

                if (image != null)
                {
                    knive.ImagePath = image.ImagePath;
                }
            }

            return Ok(knives);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Knive>> GetKnive(int id)
        {
            var knive = await context.Knives.FindAsync(id);

            if (knive == null)
            {
                return NotFound();
            }

            return Ok(knive);
        }

        [HttpPut]
        public async Task<IActionResult> PutKnife(Knive knive)
        {
            var kniveDb = context.Knives.First(x => x.KniveId == knive.KniveId);

            kniveDb.EdgeLength = knive.EdgeLength;
            kniveDb.EdgeMade = knive.EdgeMade;
            kniveDb.EdgeThickness = knive.EdgeThickness;
            kniveDb.EdgeWidth = knive.EdgeWidth;
            kniveDb.HandleDescription = knive.HandleDescription;
            kniveDb.ImagePath = knive.ImagePath;
            kniveDb.Images = knive.Images;
            kniveDb.KniveClass = knive.KniveClass;
            kniveDb.Likes = knive.Likes;
            kniveDb.Price = knive.Price;
            kniveDb.Quantity = knive.Quantity;
            kniveDb.TotalLength = knive.TotalLength;
            
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KniveExists(knive.KniveId))
                {
                    return new JsonResult("Error");
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult("Knive saved");
        }

        [Authorize]
        [Route("AddKnive")]
        [HttpPost]
        public async Task<IActionResult> AddKnive(KniveDto knive)
        {
            try
            {
                var maxId = this.context.Knives
                    .OrderByDescending(x => x.KniveId)
                    .Take(1)
                    .Select(x => x.KniveId)
                    .SingleOrDefault();

                maxId += 10;

                var newKnive = new Knive
                {
                    KniveId = maxId,
                    EdgeWidth = knive.EdgeWidth,
                    EdgeThickness = knive.EdgeThickness,
                    EdgeMade = knive.EdgeMade,
                    EdgeLength = knive.EdgeLength,
                    HandleDescription = knive.HandleDescription,
                    TotalLength = knive.TotalLength,
                    Price = knive.Price,
                    Quantity = knive.Quantity,
                    KniveClass = (KniveClass)Enum.Parse(typeof(KniveClass), knive.KniveClass)
                };

                await this.context.Knives.AddAsync(newKnive);
                await this.context.SaveChangesAsync();

                return Ok(newKnive.KniveId);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [Route("UpdateKniveLikes/{kniveId}/{isLiked}")]
        [HttpPost]
        public async Task<IActionResult> UpdateKniveLikes(bool isLiked, int kniveId)
        {
            try
            {
                var result = string.Empty;
                var kniveDb = await this.context.Knives.FirstOrDefaultAsync(x => x.KniveId == kniveId);

                if (kniveDb != null)
                {
                    if (kniveDb.Likes > 0)
                    {
                        if (isLiked)
                        {
                            kniveDb.Likes += 1;
                        }
                        else
                        {
                            kniveDb.Likes -= 1;
                        }
                    }
                    else if (kniveDb.Likes == 0)
                    {
                        if (isLiked)
                        {
                            kniveDb.Likes += 1;
                        }
                    }
                }

                await this.context.SaveChangesAsync();
                return Ok(kniveDb);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKnife(int id)
        {
            var knive = await context.Knives.FindAsync(id);
            if (knive == null)
            {
                return NotFound();
            }

            context.Knives.Remove(knive);
            await context.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("AddImage/{kniveId}")]
        public async Task<IActionResult> AddImage(int kniveId)
        {
            var image = this.Request.Form.Files[0];

            bool isSucceeded = await this.ProcessImage(image, kniveId);

            if (!isSucceeded)
            {
                return BadRequest("Something went wrong!");
            }

            return Ok("Images added!");
        }

        [Route("AllKniveImages/{kniveId}")]
        [HttpGet]
        public IActionResult AllKniveImages(int kniveId)
        {
            var images = this.context.Images
                .Where(i => i.KniveId == kniveId)
                .Select(i => new
                {
                    imagePath = i.ImagePath,
                })
                .ToList();

            return this.Ok(images);
        }

        [Route("AdminDetails")]
        [HttpGet]
        public async Task<ActionResult<object>> GetAdminInfo()
        {
            var admin = await this.context.Users.Where(u => u.Role == "Admin").Select(x => new
            {
                phoneNumber = x.PhoneNumber,
                email = x.Email
            }).FirstAsync();

            return admin;
        }

        private bool KniveExists(int id)
        {
            return this.context.Knives.Any(e => e.KniveId == id);
        }

        private async Task<bool> ProcessImage(IFormFile image, int kniveId)
        {
            try
            {
                Knive knive = await this.context.Knives.FindAsync(kniveId);

                var newImage = new KniveImage();
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
                knive.Images.Add(newImage);

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
