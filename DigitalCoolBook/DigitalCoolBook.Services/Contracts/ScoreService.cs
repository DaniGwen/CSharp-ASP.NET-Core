using DigitalCoolBook.App.Data;
using DigitalCoolBook.Models;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services.Contracts
{
    public class ScoreService : IScoreService
    {
        private readonly ApplicationDbContext context;

        public ScoreService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Score> GetScoreAsync(string scoreId)
        {
            return await this.context.Scores.FindAsync(scoreId);
        }

        public async Task AddScoreAsync(Score score)
        {
            await this.context.Scores.AddAsync(score);
            await this.context.SaveChangesAsync();
        }

        public async Task AddScoreStudentAsync(ScoreStudent scoreStudent)
        {
            await this.context.ScoreStudents.AddAsync(scoreStudent);
            await this.context.SaveChangesAsync();
        }
    }
}
