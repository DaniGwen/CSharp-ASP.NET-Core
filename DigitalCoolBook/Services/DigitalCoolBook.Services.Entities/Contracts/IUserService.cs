using DigitalCoolBook.Models;
using DigitalCoolBook.Web.Models.TestviewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services.Contracts
{
    public interface IUserService
    {

        IQueryable<Student> GetStudents();

        Task<Student> GetStudentAsync(string id);

        IQueryable<Teacher> GetTeachers();

        Task<Teacher> GetTeacherAsync(string id);

        Task RemoveStudentAsync(string id);

        Task RemoveTeacherAsync(string id);

        Task SaveChangesAsync();

        Task<IdentityUser> GetUserAsync(string id);

        IdentityUser GetUserByEmail(string email);

        Task SetStudentFinishedTestRoom(string userName);

        Task<List<StudentTestSummaryViewModel>> GetTestResultsById(string testId);

        //Task AddScorePoints(int points)
    }
}