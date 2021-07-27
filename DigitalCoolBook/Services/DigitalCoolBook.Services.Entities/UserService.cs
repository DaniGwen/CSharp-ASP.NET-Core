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
        private readonly ApplicationDbContext dbContext;

        public UserService(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<Student> GetStudentAsync(string id)
        {
            return await this.dbContext.Students.FindAsync(id);
        }

        public IQueryable<Student> GetStudents()
        {
            return this.dbContext.Students;
        }

        public IQueryable<Teacher> GetTeachers()
        {
            return this.dbContext.Teachers;
        }

        public async Task<Teacher> GetTeacherAsync(string id)
        {
            return await this.dbContext.Teachers.FindAsync(id);
        }

        public async Task RemoveStudentAsync(string id)
        {
            var student = await this.GetStudentAsync(id);

            this.dbContext.Students.Remove(student);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IdentityUser> GetUserAsync(string id)
        {
            return await this.dbContext.Users.FindAsync(id);
        }

        public async Task RemoveTeacherAsync(string id)
        {
            var teacher = await this.GetTeacherAsync(id);
            this.dbContext.Teachers.Remove(teacher);
            await this.SaveChangesAsync();
        }

        public IdentityUser GetUserByEmail(string email)
        {
            return dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public async Task SetStudentFinishedTestRoom(string studentName)
        {
            var studentDb = await this.dbContext.Students.FirstAsync(x => x.Name == studentName);
            var testRoomStudent = await this.dbContext.TestRoomStudents.FirstAsync(x => x.StudentId == studentDb.Id);
            testRoomStudent.Finished = true;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<List<StudentTestSummaryViewModel>> GetTestResultsById(string testId)
        {
            var testRoom = await this.dbContext.TestRooms
                .FirstAsync(x => x.TestId == testId);

            var studentsInTestRoom = await this.dbContext.TestRoomStudents
                .Where(x => x.TestRoomId == testRoom.Id)
                .Select(x => new StudentTestSummaryViewModel
                {
                    StudentName = x.StudentName,
                    Score = x.Score,
                })
                .ToListAsync();

            return studentsInTestRoom;
        }
    }
}
