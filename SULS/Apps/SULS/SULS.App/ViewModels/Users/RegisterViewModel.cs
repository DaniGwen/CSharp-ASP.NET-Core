using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.ViewModels.Users
{
    public class RegisterViewModel
    {
        [RequiredSis]
        [StringLengthSis(5,20,"Username must be between 5 and 20 characters.")]
        public string Username { get; set; }

        [EmailSis]
        public string Email { get; set; }

        [StringLengthSis(6,20,"Password must be between 6 and 20 characters.")]
        public string Password { get; set; }
        
        public string ConfirmPassword { get; set; }
    }
}
