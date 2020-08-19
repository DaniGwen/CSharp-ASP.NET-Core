
using RentCargoBus.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentCargoBus.Services.Contracts
{
    public interface IVanService
    {
        public Task<Van> GetVanById(string vanId);

        public List<Van> GetAllVans();

        public List<VanImage> GetImages();
    }
}
