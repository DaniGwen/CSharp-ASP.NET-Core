namespace DigitalCoolBook.Service
{
    using DigitalCoolBook.App.Data;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TestService : ITestService
    {
        private readonly ApplicationDbContext context;

        public TestService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddExpiredTestAsync(ExpiredTest expiredTest)
        {
            await this.context.ExpiredTests.AddAsync(expiredTest);
            await this.SaveChangesAsync();
        }

        public async Task AddTestAsync(Test test)
        {
            await this.context.Tests.AddAsync(test);
            await this.SaveChangesAsync();
        }

        public async Task AddTestStudentsAsync(List<TestStudent> testStudentList)
        {
            await this.context.AddRangeAsync(testStudentList);
            await this.SaveChangesAsync();
        }

        public async Task AddTestStudentsAsync(ICollection<TestStudent> testStudents)
        {
            await this.context.TestStudents.AddRangeAsync(testStudents);
            await this.SaveChangesAsync();
        }

        public async Task<Test> GetTestAsync(string id)
        {
            return await this.context.Tests.FindAsync(id);
        }

        public Test GetTestByLesson(string id)
        {
            return this.context.Tests.FirstOrDefault(t => t.LessonId == id);
        }

        public IQueryable<Test> GetTests()
        {
            return this.context.Tests;
        }

        public async Task RemoveTestAsync(string testId)
        {
            var test = await this.context.Tests.FindAsync(testId);
            this.context.Remove(test);
            await this.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }

        public async Task<ExpiredTest> GetExpiredTest(string id)
        {
            return await this.context.ExpiredTests.FindAsync(id);
        }

        public IQueryable<ExpiredTest> GetExpiredTests()
        {
            return this.context.ExpiredTests;
        }

        public async Task RemoveExpiredTest(string id)
        {
            var expiredTest = await this.context.ExpiredTests.FindAsync(id);
            this.context.ExpiredTests.Remove(expiredTest);
            await this.SaveChangesAsync();
        }

        // Add students to test room
        public async Task AddTestRoom(string[] students, string teacherId)
        {
            var testRoom = new TestRoom
            {
                TeacherId = teacherId,
            };

            foreach (var studentId in students)
            {
                var studentFromDb = this.context.Students.FirstOrDefault(s => s.Id == studentId);

                testRoom.Students.Add(studentFromDb);
            }

            await this.context.TestRooms.AddAsync(testRoom);
            await this.SaveChangesAsync();
        }
    }
}
