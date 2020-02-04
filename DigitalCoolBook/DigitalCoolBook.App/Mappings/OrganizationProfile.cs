using AutoMapper;
using DigitalCoolBook.App.Models.GradesViewModels;
using DigitalCoolBook.App.Models.StudentViewModels;
using DigitalCoolBook.Models;

namespace DigitalCoolBook.Services.Mapping
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Student, StudentEditViewModel>();
            CreateMap<StudentEditViewModel, Student>();
            CreateMap<StudentRegisterModel, Student>();
            CreateMap<Grade, GradeViewModel>();
            CreateMap<Student, StudentChangePasswordViewModel>();
        }
    }
}
