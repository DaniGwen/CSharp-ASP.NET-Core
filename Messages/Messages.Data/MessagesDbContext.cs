using Microsoft.EntityFrameworkCore;

namespace Messages.Data
{
    public class MessagesDbContext : DbContext
    {
        public MessagesDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
