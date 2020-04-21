using SULS.Data;
using SULS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.Services
{
    public class UserService : IUserService
    {
        private readonly SULSContext context;

        public UserService(SULSContext context)
        {
            this.context = context;
        }

        public User GetUser(string username, string password)
        {
            
        }

        public string HashPassword(string password)
        {
            throw new NotImplementedException();
        }
    }
}
