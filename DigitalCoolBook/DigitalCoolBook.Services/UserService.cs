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
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Student> GetStudentAsync(string id)
        {
           return await this.context.Students.FindAsync(id);
        }

        public IQueryable<Student> GetStudents()
        {
           return this.context.Students;
        }

        public IQueryable<Teacher> GetTeachers()
        {
            return this.context.Teachers;
        }

        public async Task<Teacher> GetTeacherAsync(string id)
        {
            return await this.context.Teachers.FindAsync(id);
        }

        public async Task RemoveStudentAsync(string id)
        {
            var student = await this.GetStudentAsync(id);

            this.context.Students.Remove(student);
            await this.context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }

        public async Task<IdentityUser> GetUserAsync(string id)
        {
            return await this.context.Users.FindAsync(id);
        }

        public async Task RemoveTeacherAsync(string id)
        {
            var teacher = await this.GetTeacherAsync(id);
            this.context.Teachers.Remove(teacher);
            await this.SaveChangesAsync();
        }
    }
}
