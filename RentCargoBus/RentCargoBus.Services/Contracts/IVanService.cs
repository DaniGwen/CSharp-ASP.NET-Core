
using RentCargoBus.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentCargoBus.Services.Contracts
{
    public interface IVanService
    {
        public Task<Van> GetVanById(int vanId);

        public List<Van> GetAllVans();

        public List<VanImage> GetAllImages();

        Task AddVanAsync(Van van);

        Task RemoveVanByIdAsync(int id);

        List<VanImage> GetImagesById(int id);
    }
}
