using DigitalCoolBook.App.Data;
using DigitalCoolBook.Models;
using DigitalCoolBook.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student> GetStudentAsync(string id)
        {
            var student = await _context.Students.FindAsync(id);
            return student;
        }

        public IEnumerable<Student> GetStudents()
        {
            var students = _context.Students.ToList();

            return students;
        }

        public IEnumerable<Teacher> GetTeachers()
        {
            var teachers = _context.Teachers.ToList();

            return teachers;
        }

        public async Task<Teacher> GetTeacherAsync(string id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            return teacher;
        }
    }
}
