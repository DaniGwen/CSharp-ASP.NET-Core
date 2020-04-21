using SULS.Data;
using SULS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public void AddUser(string username, string email, string password)
        {
            var user = new User
            {
                Email = email,
                Password = this.HashPassword(password),
                Username = username,
            };

            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        public User GetUserOrNull(string username, string password)
        {
            var hashedPassword = this.HashPassword(password);

            var user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);

            return user;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                return Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}
