using Microsoft.EntityFrameworkCore;

namespace DigitalCoolBook.Services
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
        private readonly ApplicationDbContext _dbContext;
        private readonly IQuestionService _questionService;

        public TestService(ApplicationDbContext context, IQuestionService questionService)
        {
            _dbContext = context;
            _questionService = questionService;
        }

        public async Task AddExpiredTestAsync(ExpiredTest expiredTest)
        {
            await _dbContext.ExpiredTests.AddAsync(expiredTest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddTestAsync(Test test)
        {
            await _dbContext.Tests.AddAsync(test);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddTestStudentsAsync(List<TestStudent> testStudentList)
        {
            var testStudentsForDb = new List<TestStudent>();

            foreach (var testStudent in testStudentList)
            {
                var testStudentFromDb = _dbContext
                    .TestStudents
                    .FirstOrDefault(ts => ts.StudentId == testStudent.StudentId && ts.TestId == testStudent.TestId);

                if (testStudentFromDb == null)
                {
                    testStudentsForDb.Add(testStudent);
                }
            }

            await _dbContext.AddRangeAsync(testStudentsForDb);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddTestStudentsAsync(ICollection<TestStudent> testStudents)
        {
            await _dbContext.TestStudents.AddRangeAsync(testStudents);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Test> GetTestAsync(string id)
        {
            return await _dbContext.Tests.FindAsync(id);
        }

        public Test GetTestByLesson(string id)
        {
            return this._dbContext.Tests.FirstOrDefault(t => t.LessonId == id);
        }

        public IQueryable<Test> GetTests()
        {
            return this._dbContext.Tests;
        }

        public async Task RemoveTestAsync(string testId)
        {
            var test = await _dbContext.Tests.FindAsync(testId);
            _dbContext.Remove(test);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ExpiredTest> GetExpiredTest(string expiredTestId)
        {
            return await _dbContext.ExpiredTests.FindAsync(expiredTestId);
        }

        public IQueryable<ExpiredTest> GetExpiredTests()
        {
            return this._dbContext.ExpiredTests;
        }

        public async Task RemoveExpiredTest(string expiredTestId, string studentId)
        {
            var expiredTestDb = await _dbContext.ExpiredTests
                .FirstOrDefaultAsync(x => x.ExpiredTestId == expiredTestId && x.StudentId == studentId);

            _dbContext.ExpiredTests.Remove(expiredTestDb);
            await _dbContext.SaveChangesAsync();
        }

        // Add students to test room
        public async Task<string> AddTestRoomAsync(string[] students, string teacherId, string testId)
        {
            var testRoom = new TestRoom
            {
                TeacherId = teacherId,
                TestId = testId,
            };

            var studentsDb = _dbContext.Students.ToList();

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

            await _dbContext.TestRooms.AddAsync(testRoom);
            await _dbContext.TestRoomStudents.AddRangeAsync(testRoom.Students);
            await _dbContext.SaveChangesAsync();

            return testRoom.Id;
        }

        public async Task TestRoomStudentFinished(string studentId, int score)
        {
            var testRoomStudent = await _dbContext.TestRoomStudents
                .FirstOrDefaultAsync(x => x.StudentId == studentId);

            if (testRoomStudent != null)
            {
                testRoomStudent.Finished = true;
                testRoomStudent.Score = score;

                await _dbContext.SaveChangesAsync();
            }
        }

        public string IsStudentInTest(string studentId)
        {
            return _dbContext.TestRoomStudents
                .Where(student => student.StudentId == studentId)
                .Select(student => student.TestRoom.TestId).FirstOrDefault();
        }

        public bool CheckAllFinished()
        {
            return _dbContext.TestRoomStudents.Any(x => x.Finished == false);
        }

        public TestRoomStudent GetTestRoomStudent(string studentId)
        {
            return _dbContext.TestRoomStudents
                .FirstOrDefault(x => x.StudentId == studentId);
        }

        public async Task RemoveTestRoomAsync(string testId)
        {
            var testRoom = _dbContext.TestRooms.FirstOrDefault(x => x.TestId == testId);

            var testRoomStudents = _dbContext.TestRoomStudents
                .Where(x => x.TestRoomId == testRoom.Id)
                .ToList();

            _dbContext.TestRooms.Remove(testRoom);
            _dbContext.TestRoomStudents.RemoveRange(testRoomStudents);
            await _dbContext.SaveChangesAsync();
        }

        public List<Test> GetActiveTestsByTeacherId(string teacherId)
        {
            var testRooms = _dbContext.TestRooms
                .Where(x => x.TeacherId == teacherId)
                .ToList();

            var tests = new List<Test>();

            foreach (var testRoom in testRooms)
            {
                var testFromDb = _dbContext.Tests.FirstOrDefault(x => x.TestId == testRoom.TestId);
                tests.Add(testFromDb);
            }

            return tests;
        }

        public async Task<List<string>> GetStudentsInTestRoomAsync(string testId)
        {
            var testRoom = _dbContext.TestRooms
                .FirstOrDefault(x => x.TestId == testId);

            var studentsInRoom = _dbContext.TestRoomStudents
                .Where(x => x.TestRoomId == testRoom.Id)
                .ToList();

            var studentNames = new List<string>();

            foreach (var student in studentsInRoom)
            {
                var studentDb = await _dbContext.Students.FindAsync(student.StudentId);
                studentNames.Add(studentDb.Name);
            }

            return studentNames;
        }

        public async Task AddArchivedTest(ArchivedTestViewModel model)
        {
            _dbContext.ArchivedTests.Add(new ArchivedTest
            {
                TestId = model.TestId,
                TestName = model.TestName,
                Date = model.Date,
                LessonId = model.LessonId,
                Place = model.Place,
                Students = model.Students,
                TeacherId = model.TeacherId,
                Timer = model.Timer
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ArchivedTestViewModel>> GetArchivedTestsByTeacherId(string teacherId)
        {
            var archivedTests = await _dbContext.ArchivedTests
                .Where(x => x.TeacherId == teacherId)
                .Select(x => new ArchivedTestViewModel
                {
                    TestName = x.TestName,
                    TestId = x.TestId,
                    Date = x.Date,
                })
                .ToListAsync();

            return archivedTests;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
