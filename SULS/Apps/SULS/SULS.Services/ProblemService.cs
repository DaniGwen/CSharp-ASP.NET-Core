using SULS.Data;
using SULS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SULS.Services
{
    public class ProblemService : BaseService,IProblemService
    {
        public ProblemService(SULSContext context) : base(context)
        {
        }

        public void AddProblem(string name, int points, string userId)
        {
            var problem = new Problem
            {
                Name = name,
                Points = points,
                UserId = userId,
            };

           base.context.Problems.Add(problem);
           base.context.SaveChanges();
        }

        public Problem GetProblemById(string problemId)
        {
            return this.context.Problems.FirstOrDefault(p => p.Id == problemId);
        }

        public IQueryable<Problem> GetProblemsByUserId(string userId)
        {
            return base.context.Problems
                .Where(u => u.UserId == userId);
        }
    }
}
