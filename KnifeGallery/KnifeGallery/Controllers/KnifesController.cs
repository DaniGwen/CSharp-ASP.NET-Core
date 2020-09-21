using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnifeGallery.Data;
using KnifeGallery.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnifeGallery.Web.Controllers
{
    [Route("api/Knifes")]
    [Produces("application/json")]
    [ApiController]
    public class KnifesController : ControllerBase
    {
        private readonly GalleryDbContext context;

        public KnifesController(GalleryDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Knife> GetKnifes()
        {
            return this.context.Knifes;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKnife([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var knife = this.context.Knifes.SingleOrDefault(i => i.KnifeId == id);

            if (knife == null)
            {
                return NotFound();
            }

            return Ok(knife);

        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddKnife(Knife knife)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await this.context.Knifes.AddAsync(knife);

                return Ok("Successfuly added.");
            }
            catch (Exception)
            {
                return BadRequest("There was a problem adding knife.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKnife([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var knife = await context.Knifes.SingleOrDefaultAsync(k => k.KnifeId == id);

            if (knife == null)
            {
                return NotFound();
            }

            this.context.Knifes.Remove(knife);
            await this.context.SaveChangesAsync();

            return Ok(knife);
        }
    }
}
