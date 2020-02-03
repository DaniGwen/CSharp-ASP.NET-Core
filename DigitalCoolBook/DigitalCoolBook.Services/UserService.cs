//using DigitalCoolBook.App.Data;
//using DigitalCoolBook.Services.Contracts;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace DigitalCoolBook.Services
//{
//    public class UserService : IUserService
//    {
//        private readonly ApplicationDbContext _context;

//        public UserService(ApplicationDbContext context)
//        {
//            _context = context;
//        }
//        public async Task<TViewModel> GetStudentAsync<TViewModel>(string id)
//        {
//            var student = await _context.Students.FindAsync(id).To<TViewModel>;
//            return student;
//        }

//        public IEnumerable<TViewModel> GetStudents<TViewModel>()
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<TViewModel> GetTeacher<TViewModel>(string id)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<TViewModel> GetTeachers<TViewModel>()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
