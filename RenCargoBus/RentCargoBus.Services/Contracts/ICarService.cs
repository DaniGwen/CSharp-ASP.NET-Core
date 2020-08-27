using RentCargoBus.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentCargoBus.Services.Contracts
{
    public interface ICarService
    {
        Task<Car> GetCarByIdAsync(int carId);

        List<Car> GetAllCars();

        List<CarImage> GetAllImages();

        Task AddCarAsync(Car car);

        Task RemoveCarByIdAsync(int id);

        Task RemoveCarImagesByIdAsync(List<int> imagesId);

        Task SaveChangesAsync();

        Task<List<CarImage>> GetImagesByCarIdAsync(int carId);
    }
}
