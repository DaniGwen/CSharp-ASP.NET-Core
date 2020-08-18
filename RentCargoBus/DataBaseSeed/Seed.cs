using RentCargoBus.Data.Models;
using RentCargoBus.Data.Models.Enum;
using System;
using System.Collections.Generic;

namespace DataBaseSeed
{
    public static class Seed
    {
        public static List<Van> SeedCargoVans()
        {
            var vans = new List<Van>();

            string[] brands = new string[] { "Ford", "Iveco", "Volkswagen", "Peugeot", "Nissan", "Skoda" };

            string[] names = new string[] { "Transit", "GDU", "Sharan", "Boxer", "e-NV200", "Octavia" };

            var images = new List<string> {
                "https://connecteam.com/wp-content/uploads/2018/03/dodge-promaster.jpg",
                "https://s.aolcdn.com/commerce/autodata/images/USC80CHV332A021001.jpg",
                "https://i.pinimg.com/originals/ce/f4/4c/cef44c1943213943fbef7313fddd2a87.jpg",
            "https://images.autotrader.com/scaler/620/420/cms/images/best-cars/2014/06-june/6-cargo-vans/226698.jpg",
            "https://cdn1.commercialtrucktrader.com/v1/media/5f19aef9506eb661c056ea45.jpg?width=512&height=384&quality=60&bestfit=true&upsize=true&blurBackground=true&blurValue=100",
            "https://az96929.vo.msecnd.net/img/01113/inventory/JKA94527/large/2018-ford-transit-350-empty-cargo-van-21.jpg?1332082488"};

            Random random = new Random();

            for (int i = 0; i < 6; i++)
            {
                vans.Add(new Van
                {
                    Brand = brands[i],
                    Height = 2,
                    Weight = 3500,
                    Name = names[i],
                    HirePrice = random.Next(30, 200),
                    MaxLoad = random.Next(1000, 2000),
                    Type = VanType.Cargo,
                    Images = new List<VanImage> { new VanImage { ImageLink = images[i] } }
                });
            }

            return vans;
        }

        public static List<Van> SeedPassangerVans()
        {
            var vans = new List<Van>();

            string[] brands = new string[] { "Ford", "Iveco", "Toyota", "Peugeot", "Nissan", "Hyndai" };

            string[] names = new string[] { "Transport", "Passanger", "Carrina", "Boxer-e", "e-NV200", "HMP" };

            var images = new List<string> {
                "https://connecteam.com/wp-content/uploads/2018/03/dodge-promaster.jpg",
                "https://s.aolcdn.com/commerce/autodata/images/USC80CHV332A021001.jpg",
                "https://i.pinimg.com/originals/ce/f4/4c/cef44c1943213943fbef7313fddd2a87.jpg",
            "https://images.autotrader.com/scaler/620/420/cms/images/best-cars/2014/06-june/6-cargo-vans/226698.jpg",
            "https://cdn1.commercialtrucktrader.com/v1/media/5f19aef9506eb661c056ea45.jpg?width=512&height=384&quality=60&bestfit=true&upsize=true&blurBackground=true&blurValue=100",
            "https://az96929.vo.msecnd.net/img/01113/inventory/JKA94527/large/2018-ford-transit-350-empty-cargo-van-21.jpg?1332082488"};

            Random random = new Random();

            for (int i = 0; i < 6; i++)
            {
                vans.Add(new Van
                {
                    Brand = brands[i],
                    Height = 2,
                    Seats = random.Next(5, 20),
                    Weight = 3500,
                    Name = names[i],
                    HirePrice = random.Next(30, 200),
                    MaxLoad = random.Next(1000, 2000),
                    Type = VanType.Passenger,
                    Images = new List<VanImage> { new VanImage { ImageLink = images[i] } }
                }); ;
            }

            return vans;
        }

        public static List<Car> SeedCars()
        {
            var cars = new List<Car>();

            string[] brands = new string[] { "Dodge", "BMW", "Volkswagen", "Opel", "Nissan", "Dacia" };

            string[] names = new string[] { "Viper", "320", "Passat", "Astra", "GTR", "Duster" };

            var images = new List<string> {
                "https://hips.hearstapps.com/hmg-prod/amv-prod-cad-assets/images/02q2/267341/2003-dodge-viper-srt-10-photo-9888-s-original.jpg?fill=2:1&resize=1200:*",
                "https://www.autofecc.com/uploads/images/cars/big/BMW-320-8324-1.jpg?mode=crop&width=620&height=465",
                "https://usedcars.motopfohe.com/files/veh146586/vw-cc-otpred-otlqvo.jpg",
            "https://img.drivemag.net/media/default/0001/31/thumb_30518_default_large.jpeg",
            "https://cdn.carbuzz.com/gallery-images/1600/699000/800/699849.jpg",
            "https://parkers-images.bauersecure.com/gallery-image/pagefiles/268788/static-exterior/1752x1168/dacia-duster-bi-fuel-02.jpg"};

            Random random = new Random();

            for (int i = 0; i < 6; i++)
            {
                cars.Add(new Car
                {
                    Brand = brands[i],
                    Name = names[i],
                    Doors = 4,
                    MilesPerGallon = random.Next(6, 20),
                    Weight = random.Next(1200, 2200),
                    HirePrice= random.Next(50, 250),
                    Images = new List<CarImage> { new CarImage { ImageLink = images[i] } }
                });
            }

            return cars;
        }
    }
}
