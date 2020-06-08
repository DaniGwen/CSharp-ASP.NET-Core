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
        private readonly IQuestionService questionService;

        public TestService(ApplicationDbContext context, IQuestionService questionService)
        {
            this.context = context;
            this.questionService = questionService;
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
            var testStudentsForDb = new List<TestStudent>();

            foreach (var testStudent in testStudentList)
            {
                var testStudentFromDb = this.context
                    .TestStudents
                    .FirstOrDefault(ts => ts.StudentId == testStudent.StudentId && ts.TestId == testStudent.TestId);

                if (testStudentFromDb == null)
                {
                    testStudentsForDb.Add(testStudent);
                }
            }

            await this.context.AddRangeAsync(testStudentsForDb);
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
        public async Task<string> AddTestRoomAsync(string[] students, string teacherId, string testId)
        {
            var testRoom = new TestRoom
            {
                TeacherId = teacherId,
                TestId = testId,
            };

            var testRoomStudentsList = new List<TestRoomStudent>();

            foreach (var studentId in students)
            {
                var studentFromDb = this.context.Students.FirstOrDefault(s => s.Name == studentId);

                var studentForTestRoom = new TestRoomStudent
                {
                    StudentId = studentFromDb.Id,
                    TestRoomId = testRoom.Id,
                    StudentName = studentFromDb.Name,
                };

                testRoomStudentsList.Add(studentForTestRoom);
            }

            await this.context.TestRooms.AddAsync(testRoom);
            await this.context.TestRoomStudents.AddRangeAsync(testRoomStudentsList);
            await this.SaveChangesAsync();

            return testRoom.Id;
        }

        public string IsStudentInTest(string studentId)
        {
            return this.context.TestRoomStudents
                .Where(student => student.StudentId == studentId)
                .Select(student => student.TestRoom.TestId).FirstOrDefault();
        }

        public bool CheckAllFinished()
        {
            return this.context.TestRoomStudents.Any(x => x.Finished == false);
        }

        public TestRoomStudent GetTestRoomStudent(string studentId)
        {
            return this.context.TestRoomStudents
                .FirstOrDefault(x => x.StudentId == studentId);
        }

        public async Task RemoveTestRoomAsync(string testId)
        {
            var testRoom = this.context.TestRooms.FirstOrDefault(x => x.TestId == testId);

            var testRoomStudents = this.context.TestRoomStudents
                .Where(x => x.TestRoomId == testRoom.Id)
                .ToList();

            this.context.TestRooms.Remove(testRoom);
            this.context.TestRoomStudents.RemoveRange(testRoomStudents);
            await this.SaveChangesAsync();
        }

        public List<Test> GetActiveTestsByTeacherId(string teacherId)
        {
            var testRooms = this.context.TestRooms
                .Where(x => x.TeacherId == teacherId)
                .ToList();

            var tests = new List<Test>();

            foreach (var testRoom in testRooms)
            {
                var testFromDb = this.context.Tests.FirstOrDefault(x => x.TestId == testRoom.TestId);
                tests.Add(testFromDb);
            }

            return tests;
        }

        public async Task<List<string>> GetStudentsInTestRoomAsync(string testId)
        {
            var testRoom = this.context.TestRooms
                .First(x => x.TestId == testId);

            var studentsInRoom = this.context.TestRoomStudents
                .Where(x => x.TestRoomId == testRoom.Id)
                .ToList();

            var studentNames = new List<string>();

            foreach (var student in studentsInRoom)
            {
                var studentDb =  await this.context.Students.FindAsync(student.StudentId);
                studentNames.Add(studentDb.Name);
            }

            return studentNames;
        }
    }
}
