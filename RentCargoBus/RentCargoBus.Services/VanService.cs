using RentCargoBus.Data;
using RentCargoBus.Data.Models;
using RentCargoBus.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Van> GetVanByIdAsync(int vanId)
        {
            var van = await this.context.Vans.FindAsync(vanId);

            return van;
        }

        public List<Van> GetAllVans()
        {
            var vans = this.context.Vans.ToList();

            return vans;
        }

        public List<VanImage> GetAllImages()
        {
            return this.context.VanImages.ToList();
        }

        public async Task AddVanAsync(Van van)
        {
            await this.context.Vans.AddAsync(van);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveVanByIdAsync(int vanId)
        {
            var van = await this.context.Vans.FindAsync(vanId);
            this.context.Vans.Remove(van);
            await this.context.SaveChangesAsync();
        }

        public List<VanImage> GetImagesByVanId(int vanId)
        {
            return this.context.VanImages
                .Where(i => i.VanId == vanId)
                .ToList();
        }

        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveVanImagesById(List<int> imagesId)
        {
            var imagesToDelete = new List<VanImage>();

            foreach (var id in imagesId)
            {
                imagesToDelete
                    .Add(this.context.VanImages.First(i => i.ImageId == id));
            }

            this.context.VanImages.RemoveRange(imagesToDelete);

            await this.SaveChangesAsync();
        }
    }
}
