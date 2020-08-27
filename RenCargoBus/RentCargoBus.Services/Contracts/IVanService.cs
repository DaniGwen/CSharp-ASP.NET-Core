
using RentCargoBus.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentCargoBus.Services.Contracts
{
    public interface IVanService
    {
        public Task<Van> GetVanByIdAsync(int vanId);

        public List<Van> GetAllVans();

        public List<VanImage> GetAllImages();

        Task AddVanAsync(Van van);

        Task RemoveVanByIdAsync(int id);

        List<VanImage> GetImagesByVanId(int id);

        Task SaveChangesAsync();

        Task RemoveVanImagesById(List<int> imagesId);
    }
}
