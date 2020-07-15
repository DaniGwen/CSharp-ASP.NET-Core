using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentCargoBus.Data.Models;
using System;

namespace RentCargoBus.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Van> Vans { get; set; }
        public DbSet<VanImage> VanImages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
