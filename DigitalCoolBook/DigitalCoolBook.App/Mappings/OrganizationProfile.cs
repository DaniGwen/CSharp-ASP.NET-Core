﻿namespace DigitalCoolBook.Services.Mapping
{
    using AutoMapper;
    using DigitalCoolBook.App.Models.GradesViewModels;
    using DigitalCoolBook.App.Models.StudentViewModels;
    using DigitalCoolBook.App.Models.TeacherViewModels;
    using DigitalCoolBook.Models;

    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            // Student mappings
            this.CreateMap<Student, StudentEditViewModel>();
            this.CreateMap<StudentEditViewModel, Student>();
            this.CreateMap<StudentRegisterModel, Student>();
            this.CreateMap<Student, StudentDetailsViewModel>();
            this.CreateMap<Student, StudentChangePasswordViewModel>();

            // Grade mappings
            this.CreateMap<Grade, GradeViewModel>();

            // Teacher mappings
            this.CreateMap<TeacherRegisterModel, Teacher>();
            this.CreateMap<Teacher, TeacherDetailsViewModel>();
            this.CreateMap<TeacherDetailsViewModel, Teacher>();
            this.CreateMap<Teacher, TeacherEditViewModel>();
            this.CreateMap<Teacher, TeacherChangePasswordViewModel>();
        }
    }
}
