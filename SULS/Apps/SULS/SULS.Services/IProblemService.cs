using SULS.Models;
using System.Linq;

namespace SULS.Services
{
    public interface IProblemService
    {
        void AddProblem(string name, int points, string userId);

        IQueryable<Problem> GetProblemsByUserId(string userId);
    }
}
