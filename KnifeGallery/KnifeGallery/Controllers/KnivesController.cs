using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KniveGallery.Data;
using KniveGallery.Data.Models;

namespace KniveGallery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnivesController : ControllerBase
    {
        private readonly GalleryDbContext _context;

        public KnivesController(GalleryDbContext context)
        {
            _context = context;
        }

        // GET: api/Knives
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Knive>>> GetKnives()
        {
            return await _context.Knives.ToArrayAsync();
        }

        // GET: api/Knives/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Knive>> GetKnive(int id)
        {
            var knive = await _context.Knives.FindAsync(id);

            if (knive == null)
            {
                return NotFound();
            }

            return knive;
        }

        // PUT: api/Knives/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKnife(int id, Knive knive)
        {
            if (id != knive.KniveId)
            {
                return BadRequest();
            }

            _context.Entry(knive).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KniveExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Knives
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Knive>> PostKnife(Knive knive)
        {
            _context.Knives.Add(knive);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKnife", new { id = knive.KniveId }, knive);
        }

        // DELETE: api/Knives/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Knive>> DeleteKnife(int id)
        {
            var knive = await _context.Knives.FindAsync(id);
            if (knive == null)
            {
                return NotFound();
            }

            _context.Knives.Remove(knive);
            await _context.SaveChangesAsync();

            return knive;
        }

        private bool KniveExists(int id)
        {
            return _context.Knives.Any(e => e.KniveId == id);
        }
    }
}
