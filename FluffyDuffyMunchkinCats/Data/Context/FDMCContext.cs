using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Data.DbContext
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
