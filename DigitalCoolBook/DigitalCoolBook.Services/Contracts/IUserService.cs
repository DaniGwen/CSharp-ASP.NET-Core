using DigitalCoolBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<Student> GetStudents();

        Task<Student> GetStudentAsync(string id);

        IEnumerable<Teacher> GetTeachers();

        Task<Teacher> GetTeacherAsync(string id);
    }
}