﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DigitalCoolBook.App.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult RegisterTeacher()
        {
            return View();
        }
    }
}