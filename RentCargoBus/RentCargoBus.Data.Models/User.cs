﻿using Microsoft.AspNetCore.Identity;

namespace RentCargoBus.Data.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}