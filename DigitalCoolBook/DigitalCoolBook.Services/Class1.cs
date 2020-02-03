using System;
using System.Collections.Generic;

namespace DigitalCoolBook.Services
{
    public interface IUserService
    {
        IEnumerable<TViewModel> GetStudents<TViewModel>();

        IEnumerable<TViewModel> GetStudent<TViewModel>();
 
        IEnumerable<TViewModel> GetTeachers<TViewModel>();

        IEnumerable<TViewModel> GetTeacher<TViewModel>();
    }
}