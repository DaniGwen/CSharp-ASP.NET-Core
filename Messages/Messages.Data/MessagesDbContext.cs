using Messages.Models;
using Microsoft.EntityFrameworkCore;

namespace Messages.Data
{
    public class MessagesDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public MessagesDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
