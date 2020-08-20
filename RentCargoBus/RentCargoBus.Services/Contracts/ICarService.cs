using RentCargoBus.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentCargoBus.Services.Contracts
{
    public interface ICarService
    {
        public Task<Car> GetCarById();

        public List<Car> GetAllCars();

        public List<CarImage> GetAllImages();

        public Task AddCarAsync(Car car);
    }
}
