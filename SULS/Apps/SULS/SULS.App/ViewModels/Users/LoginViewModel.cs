
using SIS.MvcFramework.Attributes.Validation;

namespace SULS.App.ViewModels.Users
{
    public class LoginViewModel
    {
        [RequiredSis]
        public string Username { get; set; }

        [RequiredSis]
        public string Password { get; set; }
    }
}
