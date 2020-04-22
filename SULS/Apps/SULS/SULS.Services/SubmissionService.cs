using SULS.Data;
using SULS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.Services
{
   public class SubmissionService : BaseService, ISubmissionService
    {
        public SubmissionService(SULSContext context):base(context)
        {
        }

        public void AddSubmission(string code, User user)
        {
            throw new NotImplementedException();
        }
    }
}
