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

        public async Task AddScoreAsync(Score score)
        {
            await this.context.Scores.AddAsync(score);
            await this.SaveChangesAsync();
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

        public IQueryable<Score> GetScores()
        {
            return this.context.Scores;
        }

        public IQueryable<ScoreStudent> GetScoreStudents()
        {
            return this.context.ScoreStudents;
        }

        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
