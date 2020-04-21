using System;
using System.Collections.Generic;

namespace SULS.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public virtual List<Submission> Submissions { get; set; }

        public virtual List<Problem> Problems { get; set; }
    }
}