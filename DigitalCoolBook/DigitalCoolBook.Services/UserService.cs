
using DigitalCoolBook.App.Data;
using DigitalCoolBook.Services.Contracts;
using System;
using System.Collections.Generic;

namespace DigitalCoolBook.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public TViewModel GetStudent<TViewModel>(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TViewModel> GetStudents<TViewModel>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TViewModel> GetTeacher<TViewModel>(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TViewModel> GetTeachers<TViewModel>()
        {
            throw new NotImplementedException();
        }

        TViewModel IUserService.GetTeacher<TViewModel>(string id)
        {
            throw new NotImplementedException();
        }
    }
}
