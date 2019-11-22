using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Panda.Domein
{
    public class PandaUser : IdentityUser
    {
        public PandaUser()
        {
            this.Packages = new List<Package>();
            this.Receipts = new List<Receipt>();
        }
        public ICollection<Package> Packages { get; set; }

        public ICollection<Receipt> Receipts { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public PandaUserRole Role { get; set; }


    }
}
