using SULS.Models;
using System.Linq;

namespace SULS.Services
{
   public interface ISubmissionService
    {
        void AddSubmission(string code, string user, string problemId);

        IQueryable<Submission> GetSubmissionsByProblemId(string problemId);

        void DeleteById(string id);
    }
}
