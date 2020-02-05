using AutoMapper;
using DigitalCoolBook.App.Models.GradesViewModels;
using DigitalCoolBook.App.Models.StudentViewModels;
using DigitalCoolBook.App.Models.TeacherViewModels;
using DigitalCoolBook.Models;

namespace DigitalCoolBook.Services.Mapping
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            //Student mappings
            CreateMap<Student, StudentEditViewModel>();
            CreateMap<StudentEditViewModel, Student>();
            CreateMap<StudentRegisterModel, Student>();
            CreateMap<Student, StudentDetailsViewModel>();
            CreateMap<Student, StudentChangePasswordViewModel>();

            //Grade mappings
            CreateMap<Grade, GradeViewModel>();

            //Teacher mappings
            CreateMap<TeacherRegisterModel, Teacher>();
        }
    }
}
