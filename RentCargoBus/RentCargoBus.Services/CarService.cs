using RentCargoBus.Data;
using RentCargoBus.Data.Models;
using RentCargoBus.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCargoBus.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext context;

        public CarService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddCarAsync(Car car)
        {
            await this.context.Cars.AddAsync(car);
            await this.context.SaveChangesAsync();
        }

        public List<Car> GetAllCars()
        {
            return this.context.Cars.ToList();
        }

        public List<CarImage> GetAllImages()
        {
            return this.context.CarImages.ToList();
        }

        public Task<Car> GetCarById()
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveCarByIdAsync(int id)
        {
           var car = await this.context.Cars.FindAsync(id);
            this.context.Cars.Remove(car);
            await this.context.SaveChangesAsync();
        }
    }
}
