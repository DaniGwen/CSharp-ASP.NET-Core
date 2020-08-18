using AutoMapper;
using RentCargoBus.Data.Models;
using RentCargoBus.Web.Models.Index;

namespace RentCargoBus.Web.Mappings
{
    public class MappingViewModels : Profile
    {
        public MappingViewModels()
        {
            this.CreateMap<Van, VansViewModel>();
            this.CreateMap<Car, CarsViewModel>();
        }
    }
}
