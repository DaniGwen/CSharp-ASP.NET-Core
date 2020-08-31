using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using RentCargoBus.Data;
using RentCargoBus.Data.Models;
using RentCargoBus.Data.Models.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBaseSeed
{
    public static class Seed
    {
        public static void SeedCargoVans(ApplicationDbContext context)
        {
            if (context.Vans.Any(v => v.Type == VanType.Cargo))
            {
                return;
            }

            var vans = new List<Van>();

            string[] brands = new string[] { "Ford", "Iveco", "Volkswagen", "Peugeot", "Nissan", "Skoda" };

            string[] names = new string[] { "Transit", "GDU", "Sharan", "Boxer", "e-NV200", "Octavia" };

            var images = new List<string> {
                "dodge-promaster.jpg",
                "USC80CHV332A021001.jpg",
                "cef44c1943213943fbef7313fddd2a87.jpg",
            "226698.jpg",
            "5f19aef9506eb661c056ea45.jpg",
            "2018-ford-transit-350-empty-cargo-van-21.jpg"};

            Random random = new Random();

            for (int i = 0; i < images.Count; i++)
            {
                vans.Add(new Van
                {
                    Brand = brands[i],
                    Height = 2,
                    Weight = 3500,
                    Model = names[i],
                    HirePrice = random.Next(30, 200),
                    MaxLoad = random.Next(1000, 2000),
                    Type = VanType.Cargo,
                    Images = new List<VanImage> { new VanImage { ImageName = images[i] } },
                    IsAvailable = false,
                    PlateNumber =
                  $"CB{random.Next(1000, 2000).ToString()}BP"
                });
            }

            context.Vans.AddRange(vans);
            context.SaveChanges();
        }

        public static void SeedPassangerVans(ApplicationDbContext context)
        {
            if (context.Vans.Any(v => v.Type == VanType.Passenger))
            {
                return;
            }

            var vans = new List<Van>();

            string[] brands = new string[] { "Ford", "Iveco", "Toyota", "Peugeot", "Nissan", "Hyndai" };

            string[] names = new string[] { "Transport", "Unknown", "Carrina", "Boxer-e", "e-NV200", "Unknown" };

            var images = new List<string> {
                "unnamed.png",
                "fa27dfce564021fb2452a930f80f2d96.jpg",
                "HTB1v89QOxTpK1RjSZR0q6zEwXXaB.jpg",
            };

            Random random = new Random();

            for (int i = 0; i < images.Count; i++)
            {
                vans.Add(new Van
                {
                    Brand = brands[i],
                    Height = 2,
                    Seats = random.Next(5, 20),
                    Weight = 3500,
                    Model = names[i],
                    HirePrice = random.Next(30, 200),
                    MaxLoad = random.Next(1000, 2000),
                    Type = VanType.Passenger,
                    Images = new List<VanImage> { new VanImage { ImageName = images[i] } },
                    IsAvailable = true,
                    PlateNumber =
                    $"CB{random.Next(1000, 2000).ToString()}BP"
                }); ;
            }

            context.Vans.AddRange(vans);
            context.SaveChanges();
        }

        public static void SeedCars(ApplicationDbContext context)
        {
            if (context.Cars.Any())
            {
                return;
            }

            var cars = new List<Car>();

            string[] brands = new string[] { "Dodge", "BMW", "Volkswagen", "Opel", "Nissan", "Dacia" };

            string[] names = new string[] { "Viper", "320", "Passat", "Astra", "GTR", "Duster" };

            var images = new List<string> {
                "viper.jpg",
                "bmw.jpg",
                "vw.jpg",
                "opel.jpeg",
                "gtr.jpg",
                "dacia.jpg"};

            Random random = new Random();

            for (int i = 0; i < images.Count; i++)
            {
                cars.Add(new Car
                {
                    Brand = brands[i],
                    Model = names[i],
                    Doors = 4,
                    MilesPerGallon = random.Next(6, 20),
                    Weight = random.Next(1200, 2200),
                    HirePrice = random.Next(50, 250),
                    Images = new List<CarImage> { new CarImage { ImageName = images[i] } },
                    IsAvailable = true,
                    PlateNumber =
                    $"CB{random.Next(1000, 2000).ToString()}BP"
                });
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }

        public static void SeedDeliveryFees(ApplicationDbContext context)
        {
            if (context.DeliveryAndDeposit.FirstOrDefault() == null)
            {
                context.DeliveryAndDeposit
                    .Add(new DeliveryAndDeposit { CarDeliveryBg = 0, CarDeliveryEu = 0, VanDeliveryEu = 0, VanDeliveryBg = 0 });
                context.SaveChanges();
            }
        }

        public static async Task SeedPhoneAndEmail(ApplicationDbContext context)
        {
            var phoneEmail = context.PhoneEmails.Any();

            if (!phoneEmail)
            {
                context.PhoneEmails.Add(new PhoneEmail
                {
                    Email = "sparoupbb@hotmail.com",
                    PhoneNumber = "+359 883 44 44 87",
                });

               await context.SaveChangesAsync();
            }
        }
    }
}
