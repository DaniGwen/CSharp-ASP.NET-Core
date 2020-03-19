using System.Threading.Tasks;

namespace DigitalCoolBook.Services.Contracts
{
    public interface IScore
    {
        Task GetScoreAsync(string scoreId);

        Task AddScoreAsync(string scoreId);

        Task AddScoreStudentAsync(string scoreStudentId);
    }
}
