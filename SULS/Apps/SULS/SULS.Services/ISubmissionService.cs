using SULS.Models;

namespace SULS.Services
{
   public interface ISubmissionService
    {
        void AddSubmission(string code, string user, string problemId);
    }
}
