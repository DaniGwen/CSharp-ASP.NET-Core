using System;
using System.Linq;
using System.Threading.Tasks;
using DigitalCoolBook.App.Data;
using DigitalCoolBook.Models;
using DigitalCoolBook.Services.Contracts;

namespace DigitalCoolBook.Services
{
    public class LiveFeedService : ILiveFeedService
    {
        private readonly ApplicationDbContext _dbContext;

        public LiveFeedService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveMessage(string userId, string userName, string message)
        {
            await _dbContext.LiveFeedMessages.AddAsync(new LiveFeedMessage
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                UserName = userName,
                Date = DateTime.UtcNow,
                Message = message
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IQueryable<LiveFeedMessage>> GetLiveMessages()
        {
            return _dbContext.LiveFeedMessages;
        }
    }
}
