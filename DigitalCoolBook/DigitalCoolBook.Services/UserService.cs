namespace DigitalCoolBook.Services
{
    using DigitalCoolBook.App.Data;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student> GetStudentAsync(string id)
        {
           return await _context.Students.FindAsync(id);
        }

        public IQueryable<Student> GetStudents()
        {
           return _context.Students;
        }

        public IQueryable<Teacher> GetTeachers()
        {
            return _context.Teachers;
        }

        public async Task<Teacher> GetTeacherAsync(string id)
        {
            return await _context.Teachers.FindAsync(id);
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
            return await _context.Users.FindAsync(id);
        }

        public async Task RemoveTeacherAsync(string id)
        {
            var teacher = await this.GetTeacherAsync(id);
            _context.Remove(teacher);
            await this.SaveChangesAsync();
        }
    }
}
