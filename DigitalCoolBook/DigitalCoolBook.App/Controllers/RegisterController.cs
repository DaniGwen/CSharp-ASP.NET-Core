using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalCoolBook.App.Controllers
{
    [Authorize(Roles =("Admin"))]
    public class RegisterController : Controller
    {
        public IActionResult RegisterTeacher()
        {
            return View();
        }

        public IActionResult RegisterStudent()
        {
            return View();
        }
    }
}