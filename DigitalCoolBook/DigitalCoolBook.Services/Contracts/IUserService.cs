﻿using System.Collections.Generic;

namespace DigitalCoolBook.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<TViewModel> GetStudents<TViewModel>();

        TViewModel GetStudentAsync<TViewModel>(string id);

        IEnumerable<TViewModel> GetTeachers<TViewModel>();

        TViewModel GetTeacher<TViewModel>(string id);
    }
}