using AutoMapper;
using DigitalCoolBook.App.Models.StudentViewModels;
using DigitalCoolBook.Models;

namespace DigitalCoolBook.Services.Mapping
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Student, StudentEditViewModel>();
            CreateMap<StudentRegisterModel, Student>();
        }
    }
}
