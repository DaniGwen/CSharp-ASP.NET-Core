using System;
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

        public async void SaveMessage(string teacherId, string message)
        {
            await _dbContext.LiveFeedMessages.AddAsync(new LiveFeedMessage
            {
                Id = Guid.NewGuid().ToString(),
                TeacherId = teacherId,
                Date = DateTime.UtcNow,
                Message = message
            });

            await _dbContext.SaveChangesAsync();
        }
    }
}
