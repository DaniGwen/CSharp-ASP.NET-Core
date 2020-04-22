﻿using SULS.Data;
using SULS.Models;
using System;

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
                User = user,
                ProblemId = problemId,
                UserId = user.Id,
            };

            this.context.Submissions.Add(submission);
            this.context.SaveChanges();
        }
    }
}
