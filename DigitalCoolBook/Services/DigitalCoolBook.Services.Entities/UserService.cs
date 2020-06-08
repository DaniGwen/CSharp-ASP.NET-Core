namespace DigitalCoolBook.Services
{
    using DigitalCoolBook.App.Data;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using DigitalCoolBook.Web.Models.TestviewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
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

        public IdentityUser GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public async Task SetStudentFinishedTestRoom(string studentName)
        {
            var studentDb = await this.context.Students.FirstAsync(x => x.Name == studentName);
            var testRoomStudent = await this.context.TestRoomStudents.FirstAsync(x => x.StudentId == studentDb.Id);
            testRoomStudent.Finished = true;

            await this.context.SaveChangesAsync();
        }

        public async Task<List<StudentTestSummaryViewModel>> GetTestResultsById(string testId)
        {
            var testRoom = this.context.TestRooms
                .First(x => x.TestId == testId);

            var studentsInTestRoom = this.context.TestRoomStudents
                .Where(x => x.TestRoomId == testRoom.Id)
                .Select(x => new StudentTestSummaryViewModel
                {
                    StudentName = x.StudentName,
                    Score = x.Score,
                })
                .ToList();

            return studentsInTestRoom;
        }
    }
}
