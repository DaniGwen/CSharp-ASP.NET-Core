namespace DigitalCoolBook.Service
{
    using DigitalCoolBook.App.Data;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using DigitalCoolBook.Web.Models.TestviewModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TestService : ITestService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IQuestionService questionService;

        public TestService(ApplicationDbContext context, IQuestionService questionService)
        {
            this.dbContext = context;
            this.questionService = questionService;
        }

        public async Task AddExpiredTestAsync(ExpiredTest expiredTest)
        {
            await this.dbContext.ExpiredTests.AddAsync(expiredTest);
            await this.SaveChangesAsync();
        }

        public async Task AddTestAsync(Test test)
        {
            await this.dbContext.Tests.AddAsync(test);
            await this.SaveChangesAsync();
        }

        public async Task AddTestStudentsAsync(List<TestStudent> testStudentList)
        {
            var testStudentsForDb = new List<TestStudent>();

            foreach (var testStudent in testStudentList)
            {
                var testStudentFromDb = this.dbContext
                    .TestStudents
                    .FirstOrDefault(ts => ts.StudentId == testStudent.StudentId && ts.TestId == testStudent.TestId);

                if (testStudentFromDb == null)
                {
                    testStudentsForDb.Add(testStudent);
                }
            }

            await this.dbContext.AddRangeAsync(testStudentsForDb);
            await this.SaveChangesAsync();
        }

        public async Task AddTestStudentsAsync(ICollection<TestStudent> testStudents)
        {
            await this.dbContext.TestStudents.AddRangeAsync(testStudents);
            await this.SaveChangesAsync();
        }

        public async Task<Test> GetTestAsync(string id)
        {
            return await this.dbContext.Tests.FindAsync(id);
        }

        public Test GetTestByLesson(string id)
        {
            return this.dbContext.Tests.FirstOrDefault(t => t.LessonId == id);
        }

        public IQueryable<Test> GetTests()
        {
            return this.dbContext.Tests;
        }

        public async Task RemoveTestAsync(string testId)
        {
            var test = await this.dbContext.Tests.FindAsync(testId);
            this.dbContext.Remove(test);
            await this.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<ExpiredTest> GetExpiredTest(string expiredTestId)
        {
            return await this.dbContext.ExpiredTests.FindAsync(expiredTestId);
        }

        public IQueryable<ExpiredTest> GetExpiredTests()
        {
            return this.dbContext.ExpiredTests;
        }

        public async Task RemoveExpiredTest(string id)
        {
            var expiredTest = await this.dbContext.ExpiredTests.FindAsync(id);
            this.dbContext.ExpiredTests.Remove(expiredTest);
            await this.SaveChangesAsync();
        }

        // Add students to test room
        public async Task<string> AddTestRoomAsync(string[] students, string teacherId, string testId)
        {
            var testRoom = new TestRoom
            {
                TeacherId = teacherId,
                TestId = testId,
            };

            var studentsDb = this.dbContext.Students.ToList();

            foreach (var studentName in students)
            {
                var studentDb = studentsDb.FirstOrDefault(s => s.Name == studentName);
                
                var studentForTestRoom = new TestRoomStudent
                {
                    StudentId = studentDb?.Id,
                    StudentName = studentDb?.Name,
                    TestRoomId = testRoom.Id,
                };
                testRoom.Students.Add(studentForTestRoom);
            }

            await this.dbContext.TestRooms.AddAsync(testRoom);
            await this.dbContext.TestRoomStudents.AddRangeAsync(testRoom.Students);
            await this.SaveChangesAsync();

            return testRoom.Id;
        }

        public string IsStudentInTest(string studentId)
        {
            return this.dbContext.TestRoomStudents
                .Where(student => student.StudentId == studentId)
                .Select(student => student.TestRoom.TestId).FirstOrDefault();
        }

        public bool CheckAllFinished()
        {
            return this.dbContext.TestRoomStudents.Any(x => x.Finished == false);
        }

        public TestRoomStudent GetTestRoomStudent(string studentId)
        {
            return this.dbContext.TestRoomStudents
                .FirstOrDefault(x => x.StudentId == studentId);
        }

        public async Task RemoveTestRoomAsync(string testId)
        {
            var testRoom = this.dbContext.TestRooms.FirstOrDefault(x => x.TestId == testId);

            var testRoomStudents = this.dbContext.TestRoomStudents
                .Where(x => x.TestRoomId == testRoom.Id)
                .ToList();

            this.dbContext.TestRooms.Remove(testRoom);
            this.dbContext.TestRoomStudents.RemoveRange(testRoomStudents);
            await this.SaveChangesAsync();
        }

        public List<Test> GetActiveTestsByTeacherId(string teacherId)
        {
            var testRooms = this.dbContext.TestRooms
                .Where(x => x.TeacherId == teacherId)
                .ToList();

            var tests = new List<Test>();

            foreach (var testRoom in testRooms)
            {
                var testFromDb = this.dbContext.Tests.FirstOrDefault(x => x.TestId == testRoom.TestId);
                tests.Add(testFromDb);
            }

            return tests;
        }

        public async Task<List<string>> GetStudentsInTestRoomAsync(string testId)
        {
            var testRoom = this.dbContext.TestRooms
                .FirstOrDefault(x => x.TestId == testId);

            var studentsInRoom = this.dbContext.TestRoomStudents
                .Where(x => x.TestRoomId == testRoom.Id)
                .ToList();

            var studentNames = new List<string>();

            foreach (var student in studentsInRoom)
            {
                var studentDb = await this.dbContext.Students.FindAsync(student.StudentId);
                studentNames.Add(studentDb.Name);
            }

            return studentNames;
        }

        public List<TestExpiredViewModel> GetExpiredTestsByTeacherId(string teacherId)
        {
            var expiredTests = this.dbContext.ExpiredTests
                .Where(x => x.TeacherId == teacherId)
                .Select(x => new TestExpiredViewModel
                {
                    ExpiredTestName = x.TestName,
                    ExpiredTestId = x.ExpiredTestId,
                    ExpiredTestDate = x.Date,
                })
                .ToList();

            return expiredTests;
        }
    }
}
