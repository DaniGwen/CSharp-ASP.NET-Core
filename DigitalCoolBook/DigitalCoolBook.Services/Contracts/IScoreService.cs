using DigitalCoolBook.Models;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services.Contracts
{
    public interface IScoreService
    {
        Task<Score> GetScoreAsync(string scoreId);

        Task AddScoreAsync(Score score);

        Task AddScoreStudentAsync(ScoreStudent scoreStudent);
    }
}
