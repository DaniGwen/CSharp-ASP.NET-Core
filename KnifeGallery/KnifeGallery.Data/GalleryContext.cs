using KniveGallery.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KniveGallery.Data
{
    public class GalleryDbContext : IdentityDbContext<IdentityUser>
    {
        public GalleryDbContext(DbContextOptions<GalleryDbContext> options) : base(options)
        {
        }

        public DbSet<Knive> Knives { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
