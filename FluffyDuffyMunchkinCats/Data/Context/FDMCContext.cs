using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Data.Context
{
    public class FDMCContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }

        public FDMCContext(DbContextOptions<FDMCContext> dbContextOptions)
            : base(dbContextOptions) 
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbConfig.SqlConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cat>()
                .Property(p => p.Breed)
                .HasMaxLength(25)
                .HasDefaultValue("Uknown");

            modelBuilder.Entity<Cat>()
                .Property(p => p.Age)
                .HasMaxLength(2);

            modelBuilder.Entity<Cat>()
                .Property(p => p.Name)
                .HasMaxLength(30);
        }
    }
}
