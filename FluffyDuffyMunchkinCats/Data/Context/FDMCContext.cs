using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Data.Context
{
    public class FDMCContext : DbContext
    {
        public FDMCContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbConfig.SqlConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
