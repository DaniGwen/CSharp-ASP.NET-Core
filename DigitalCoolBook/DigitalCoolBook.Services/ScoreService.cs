using DigitalCoolBook.App.Data;
using DigitalCoolBook.Models;
using DigitalCoolBook.Services.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services
{
    public class ScoreService : IScoreService
    {
        private readonly ApplicationDbContext context;

        public ScoreService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task AddScoreAsync(Score score)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddScoreStudentAsync(ScoreStudent scoreStudent)
        {
            await this.context.ScoreStudents.AddAsync(scoreStudent);
            await this.SaveChangesAsync();
        }

        public async Task<Score> GetScoreAsync(string scoreId)
        {
            return await this.context.Scores.FindAsync(scoreId);
        }

        public IQueryable GetScores()
        {
            return this.context.Scores;
        }

        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
