using Microsoft.EntityFrameworkCore;
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

        public async Task<Car> GetCarByIdAsync(int carId)
        {
            var car = await this.context.Cars.FindAsync(carId);

            return car;
        }

        public async Task<List<CarImage>> GetImagesByCarIdAsync(int carId)
        {
            return await this.context
                .CarImages
                .Where(i => i.CarId == carId)
                .ToListAsync();
        }

        public async Task RemoveCarByIdAsync(int id)
        {
            var car = await this.context.Cars.FindAsync(id);
            this.context.Cars.Remove(car);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveCarImagesByIdAsync(List<int> imagesId)
        {
            var imagesToDelete = new List<CarImage>();

            foreach (var id in imagesId)
            {
                imagesToDelete.Add(await this.context.CarImages.FindAsync(id));
            }

            this.context.CarImages.RemoveRange(imagesToDelete);

            await this.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
