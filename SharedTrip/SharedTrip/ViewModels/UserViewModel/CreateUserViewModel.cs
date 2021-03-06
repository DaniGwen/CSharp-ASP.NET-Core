﻿using SIS.MvcFramework.Attributes.Validation;

namespace SharedTrip.ViewModels.UserViewModel
{
    public class CreateUserViewModel
    {
        [RequiredSis]
        [StringLengthSis(5,20,"")]
        public string Username { get; set; }

        [RequiredSis]
        [EmailSis]
        public string Email { get; set; }

        [RequiredSis]
        [StringLengthSis(6, 20, "")]
        public string Password { get; set; }

        [RequiredSis]
        public string ConfirmPassword { get; set; }
    }
}
