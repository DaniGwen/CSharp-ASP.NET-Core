using SULS.Data;
using SULS.Models;
using System;
using System.Linq;

namespace SULS.Services
{
    public class SubmissionService : BaseService, ISubmissionService
    {
        private readonly IProblemService problemService;
        private readonly IUserService userService;

        public SubmissionService(
            SULSContext context,
            IProblemService problemService,
            IUserService userService) : base(context)
        {
            this.problemService = problemService;
            this.userService = userService;
        }

        public void AddSubmission(string code, string userId, string problemId)
        {
            var rnd = new Random();
            var problem = this.problemService.GetProblemById(problemId);
            var user = this.userService.GetUserById(userId);

            var submission = new Submission
            {
                Code = code,
                CreatedOn = DateTime.UtcNow,
                AchievedResult = rnd.Next(0, problem.Points),
                ProblemId = problemId,
                UserId = user.Id
            };

            this.context.Submissions.Add(submission);
            this.context.SaveChanges();
        }

        public void DeleteById(string id)
        {
            var submission = this.context.Submissions.Find(id);
            this.context.Submissions.Remove(submission);

            this.context.SaveChanges();
        }

        public IQueryable<Submission> GetSubmissionsByProblemId(string problemId)
        {
            var submissions = this.context.Submissions.Where(s => s.ProblemId == problemId);

            return submissions;
        }
    }
}
