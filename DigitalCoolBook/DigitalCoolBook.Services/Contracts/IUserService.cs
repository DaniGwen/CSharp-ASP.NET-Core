using DigitalCoolBook.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<Student> GetStudents();

        Task<Student> GetStudentAsync(string id);

        IEnumerable<Teacher> GetTeachers();

        Task<Teacher> GetTeacher(string id);

        Task RemoveStudentAsync(string id);

        Task SaveChangesAsync();

        Task<IdentityUser> GetUserAsync(string id);
    }
}