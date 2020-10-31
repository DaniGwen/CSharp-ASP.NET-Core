using System;
using System.Collections.Generic;
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

        // GET: api/Knives
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

            var knives = this.context.Knives
                .Where(knive => knive.KniveClass == kniveType)
                .ToList();

            return Ok(knives);
        }

        // GET: api/Knives/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Knive>> GetKnive(int id)
        {
            var knive = await context.Knives.FindAsync(id);

            if (knive == null)
            {
                return NotFound();
            }

            return knive;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKnife(int id, Knive knive)
        {
            if (id != knive.KniveId)
            {
                return BadRequest();
            }

            context.Entry(knive).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KniveExists(id))
                {
                    return BadRequest("Error!");
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult("Knive saved!");
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Knive>> PostKnife(Knive knive)
        {
            context.Knives.Add(knive);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetKnife", new { id = knive.KniveId }, knive);
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

            await this.ProcessImage(image, kniveId);

            return Ok();
        }

        [Route("AllKniveImages/{kniveId}")]
        [HttpGet]
        public async Task<IActionResult> AllKniveImages(int kniveId)
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

        private bool KniveExists(int id)
        {
            return this.context.Knives.Any(e => e.KniveId == id);
        }

        private async Task ProcessImage(IFormFile image, int kniveId)
        {
            Knive knive = await this.context.Knives.FindAsync(kniveId);

            var newImage = new KniveImage();

            // Save image to wwwroot / image
            string clientAppAssets = this.hostEnvironment.ContentRootPath + "/ClientApp/src/assets/images/";
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
    }
}
