using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.App.Models.BindingModels
{
    public class UserCreateBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name must have more than 3 letters", MinimumLength = 3)]
        [RegularExpression("[A-Za-z0-9_`'-\\.]")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name must have more than 3 letters", MinimumLength = 3)]
        [RegularExpression("[A-Za-z0-9_`'-\\.]")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression("^[0-9]{10}$")]
        public string UCN { get; set; }

    }
}
