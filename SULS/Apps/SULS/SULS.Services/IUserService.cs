using SULS.Models;

namespace SULS.Services
{
    public interface IUserService
    {
        User GetUserOrNull(string username, string password);

        void AddUser(string username, string email, string password);

        User GetUserById(string userId);
    }
}
