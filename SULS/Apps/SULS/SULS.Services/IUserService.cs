using SULS.Models;

namespace SULS.Services
{
    public interface IUserService
    {
        User GetUser(string username, string password);

        string HashPassword(string password);
    }
}
