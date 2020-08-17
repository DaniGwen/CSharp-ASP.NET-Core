using RentCargoBus.Data;
using RentCargoBus.Data.Models;
using RentCargoBus.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace RentCargoBus.Services
{
    public class VanService : IVanService
    {
        private ApplicationDbContext context;

        public VanService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Van> GetVanById(string vanId)
        {
            var van = await this.context.Vans.FindAsync(vanId);

            return van;
        }
    }
}
