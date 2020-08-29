using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentCargoBus.Data.Models;

namespace RentCargoBus.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Van> Vans { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<VanImage> VanImages { get; set; }

        public DbSet<CarImage> CarImages { get; set; }

        public DbSet<Rent> Rents { get; set; }

        public DbSet<RentCar> RentCars { get; set; }

        public DbSet<RentVan> RentVans { get; set; }

        public DbSet<User> ApplicationUsers { get; set; }

        public DbSet<Delivery> VehiclesDelivery { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RentCar>(x => x.HasKey(x => new { x.CarId, x.RentId }));

            builder.Entity<RentVan>(x => x.HasKey(x => new { x.VanId, x.RentId }));
        }
    }
}
