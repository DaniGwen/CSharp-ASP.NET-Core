using AutoMapper;
using Microsoft.AspNetCore.Http;
using RentAVan.Web.Areas.Identity.Pages.Account.Manage;
using RentCargoBus.Data.Models;
using RentCargoBus.Web.Areas.Identity.Pages.Account.Manage;
using RentCargoBus.Web.Models.EditViewModels;
using RentCargoBus.Web.Models.Index;

namespace RentCargoBus.Web.Mappings
{
    public class MappingViewModels : Profile
    {
        public MappingViewModels()
        {
            //Van mappings
            this.CreateMap<VanEditPostModel, Van>();
            this.CreateMap<Van, VanEditViewModel>();
            this.CreateMap<AddVanModel.InputModel, Van>().ReverseMap();
            this.CreateMap<IFormFile, VanImage>();
            this.CreateMap<Van, VansViewModel>().ReverseMap();

            //Car mappings
            this.CreateMap<Car, CarsViewModel>();
            this.CreateMap<AddCarModel.InputModel, Car>().ReverseMap();
            this.CreateMap<IFormFile, CarImage>();
            this.CreateMap<Car, CarEditViewModel>();
            this.CreateMap<CarPostViewModel, Car>();

            //Delivery mappings 
            this.CreateMap<Delivery, EditDeliveryModel.InputModel>().ReverseMap();
        }
    }
}
