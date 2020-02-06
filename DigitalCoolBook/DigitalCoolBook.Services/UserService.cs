using DigitalCoolBook.App.Data;
using DigitalCoolBook.Models;
using DigitalCoolBook.Services.Contracts;
using Microsoft.AspNetCore.Identity;
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

        public IQueryable<Student> GetStudents()
        {
            var students = _context.Students;

            return students;
        }

        public IQueryable<Teacher> GetTeachers()
        {
            var teachers = _context.Teachers;

            return teachers;
        }

        public async Task<Teacher> GetTeacherAsync(string id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            return teacher;
        }

        public async Task RemoveStudentAsync(string id)
        {
            var student = await this.GetStudentAsync(id);

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IdentityUser> GetUserAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);

            return user;
        }

        public async Task RemoveTeacherAsync(string id)
        {
            var teacher = await this.GetTeacherAsync(id);
            _context.Remove(teacher);
            await this.SaveChangesAsync();
        }
    }
}
