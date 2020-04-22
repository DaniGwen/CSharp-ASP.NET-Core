using SULS.Data;
using SULS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SULS.Services
{
    public class ProblemService : IProblemService
    {
        private readonly SULSContext context;

        public ProblemService(SULSContext context)
        {
            this.context = context;
        }

        public void AddProblem(string name, int points, string userId)
        {
            var problem = new Problem
            {
                Name = name,
                Points = points,
                UserId = userId,
            };

            this.context.Problems.Add(problem);
            this.context.SaveChanges();
        }

        public IQueryable<Problem> GetProblemsByUserId(string userId)
        {
            return this.context.Problems
                .Where(u => u.UserId == userId);
        }
    }
}
