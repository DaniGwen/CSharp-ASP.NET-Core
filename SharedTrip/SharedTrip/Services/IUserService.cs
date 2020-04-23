using SharedTrip.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
   public interface IUserService
    {
        void CreateUser(string username, string email, string password);

        User GetUserOrNull(string username, string password);
    }
}
