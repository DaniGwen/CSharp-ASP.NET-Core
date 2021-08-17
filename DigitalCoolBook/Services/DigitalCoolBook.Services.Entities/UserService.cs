﻿namespace DigitalCoolBook.Services
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
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Student> GetStudentAsync(string id)
        {
            return await _dbContext.Students.FindAsync(id);
        }

        public IQueryable<Student> GetStudents()
        {
            return _dbContext.Students;
        }

        public IQueryable<Teacher> GetTeachers()
        {
            return _dbContext.Teachers;
        }

        public async Task<Teacher> GetTeacherAsync(string id)
        {
            return await _dbContext.Teachers.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task RemoveStudentAsync(string id)
        {
            var student = await this.GetStudentAsync(id);

            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<IdentityUser> GetUserAsync(string id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task RemoveTeacherAsync(string id)
        {
            var teacher = await this.GetTeacherAsync(id);
            _dbContext.Teachers.Remove(teacher);

            await _dbContext.SaveChangesAsync();
        }

        public IdentityUser GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public async Task SetStudentFinishedTestRoom(string studentName)
        {
            var studentDb = await this._dbContext.Students.FirstAsync(x => x.Name == studentName);
            var testRoomStudent = await this._dbContext.TestRoomStudents.FirstAsync(x => x.StudentId == studentDb.Id);
            testRoomStudent.Finished = true;

            await this._dbContext.SaveChangesAsync();
        }

        public async Task<List<StudentTestSummaryViewModel>> GetTestResultsById(string testId)
        {
            var testRoom = await _dbContext.TestRooms
                .FirstAsync(x => x.TestId == testId);

            var studentsInTestRoom = await _dbContext.TestRoomStudents
                .Where(x => x.TestRoomId == testRoom.Id)
                .Select(x => new StudentTestSummaryViewModel
                {
                    Score = x.Score,
                })
                .ToListAsync();

            return studentsInTestRoom;
        }
    }
}
