using DigitalCoolBook.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services.Contracts
{
    public interface IScoreService
    {
        Task<Score> GetScoreAsync(string scoreId);

        Task AddScoreAsync(Score score);

        Task AddScoreStudentAsync(ScoreStudent scoreStudent);

        IQueryable<Score> GetScores();

        IQueryable<ScoreStudent> GetScoreStudents();

        Task CreateScore(int points, string lessonId, string studentId);
    }
}
