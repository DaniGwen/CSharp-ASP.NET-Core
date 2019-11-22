using Microsoft.AspNet.Identity.EntityFramework;
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


    }
}
