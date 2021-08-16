using System;
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

                if (testStudentFromDb == null) { testStudentsForDb.Add(testStudent); }
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
            return _dbContext.Tests.FirstOrDefault(t => t.LessonId == id);
        }

        public IQueryable<Test> GetTests()
        {
            return _dbContext.Tests;
        }

        public async Task SetTestExpiredAsync(string testId)
        {
            var testDb = _dbContext.Tests.FirstOrDefault(x =>x.TestId == testId);
            if (testDb != null) 
            {
                testDb.IsExpired = true;
            }
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ExpiredTest> GetExpiredTest(string expiredTestId)
        {
            return await _dbContext.ExpiredTests.FindAsync(expiredTestId);
        }

        public IQueryable<ExpiredTest> GetExpiredTests()
        {
            return _dbContext.ExpiredTests;
        }

        public async Task RemoveExpiredTest(string expiredTestId, string studentId)
        {
            var expiredTestDb = await _dbContext.ExpiredTests
                .FirstOrDefaultAsync(x => x.ExpiredTestId == expiredTestId && x.StudentId == studentId);

            _dbContext.ExpiredTests.Remove(expiredTestDb);
            await _dbContext.SaveChangesAsync();
        }

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
            await Task.Delay(500);
            await _dbContext.TestRoomStudents.AddRangeAsync(testRoom.Students);
            await _dbContext.SaveChangesAsync();

            return testRoom.Id;
        }

        public async Task TestRoomStudentFinished(string studentId, int score)
        {
            var testRoomStudent =  _dbContext.TestRoomStudents
                .FirstOrDefault(x => x.StudentId == studentId);

            if (testRoomStudent != null)
            {
                testRoomStudent.Finished = true;
                testRoomStudent.Score = score;
            }

            await _dbContext.SaveChangesAsync();
        }

        public string IsStudentInTest(string studentId)
        {
            return _dbContext.TestRoomStudents
                .Where(student => student.StudentId == studentId)
                .Select(student => student.TestRoom.TestId).FirstOrDefault();
        }

        public bool IsAllStudentsFinished()
        {
            return _dbContext.TestRoomStudents.All(x => x.Finished);
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
                var testFromDb = _dbContext.Tests.FirstOrDefault(x => x.TestId == testRoom.TestId && !x.IsExpired);
                tests.Add(testFromDb);
            }

            return tests;
        }

        public async Task<List<Student>> GetStudentsInTestRoomAsync(string testId)
        {
            var testRoom = _dbContext.TestRooms
                .FirstOrDefault(x => x.TestId == testId);

            var testRoomStudents = _dbContext.TestRoomStudents
                .Where(x => x.TestRoomId == testRoom.Id
                            && !x.Finished)
                .ToList();

            var students = new List<Student>();

            foreach (var testRoomStudent in testRoomStudents)
            {
                var studentDb = await _dbContext.Students
                    .FirstOrDefaultAsync(x => x.Id == testRoomStudent.StudentId);

                students.Add(studentDb);
            }

            return students;
        }

        public async Task AddArchivedTest(Test model)
        {
            var isArchivedTestDb =
                _dbContext.ArchivedTests.Any(x => x.TeacherId == model.TeacherId && x.TestId == model.TestId);

            if (!isArchivedTestDb)
            {
                await _dbContext.ArchivedTests.AddAsync(new ArchivedTest
                {
                    TestId = model.TestId,
                    TestName = model.TestName,
                    Date = DateTime.Now,
                    LessonId = model.LessonId,
                    Place = model.Place,
                    TeacherId = model.TeacherId,
                    Timer = model.Timer
                });

                await _dbContext.SaveChangesAsync();
            }
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

        public async Task<bool> IsLessonUnlocked(string categoryId, int lessonLevel, string studentId)
        {
            if (lessonLevel == 1) { return true; }

            var lessonsDb = await _dbContext.Lessons
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();

            var scoreStudents = _dbContext.ScoreStudents
                .Where(x => x.StudentId == studentId)
                .AsQueryable();

            foreach (var lessonDb in lessonsDb)
            {
                if (lessonLevel > lessonDb.Level)
                {
                    var score = scoreStudents
                                       .FirstOrDefault(x => x.Score.LessonId == lessonDb.LessonId)?.Score;

                    if (score?.ScorePoints > 70) { return true; }
                }
               
            }

            return false;
        }
    }
}
