using AutoMapper;
using Microsoft.AspNetCore.Http;
using RentCargoBus.Data.Models;
using RentCargoBus.Web.Areas.Identity.Pages.Account.Manage;
using RentCargoBus.Web.Models.Index;

namespace RentCargoBus.Web.Mappings
{
    public class MappingViewModels : Profile
    {
        public MappingViewModels()
        {
            this.CreateMap<AddVanModel.InputModel, Van>().ReverseMap();
            this.CreateMap<IFormFile, VanImage>();
            this.CreateMap<Van, VansViewModel>();
            this.CreateMap<Car, CarsViewModel>();
            this.CreateMap<AddCarModel.InputModel, Car>().ReverseMap();
            this.CreateMap<IFormFile, CarImage>();

        }
    }
}
