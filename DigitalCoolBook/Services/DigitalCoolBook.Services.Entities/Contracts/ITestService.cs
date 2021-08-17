﻿namespace DigitalCoolBook.Services.Contracts
{
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Web.Models.TestviewModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ITestService
    {
        Test GetTestByLesson(string id);

        Task AddTestAsync(Test test);

        Task AddTestStudentsAsync(List<TestStudent> testStudentList);

        IQueryable<Test> GetTests();

        Task<Test> GetTestAsync(string id);

        Task AddExpiredTestAsync(ExpiredTest expiredTest);

        Task SetTestExpiredAsync(string testId);

        Task AddTestStudentsAsync(ICollection<TestStudent> testStudents);

        Task<ExpiredTest> GetExpiredTest(string id);

        IQueryable<ExpiredTest> GetExpiredTests();

        Task RemoveExpiredTest(string expiredTestId, string studentId);

        Task RemoveTest(string testId);

        Task<string> AddTestRoomAsync(string[] students, string teacherId, string testId);

        string IsStudentInTest(string studentId);

        bool IsAllStudentsFinished();

        TestRoomStudent GetTestRoomStudent(string studentId);

        Task RemoveTestRoomAsync(string testId);

        List<Test> GetActiveTestsByTeacherId(string teacherId);

        Task<List<Student>> GetStudentsInTestRoomAsync(string testId);

        Task<List<ArchivedTestViewModel>> GetArchivedTestsByTeacherId(string teacherId);

        Task TestRoomStudentFinished(string studentId, int score);

        Task AddArchivedTest(Test archivedTestViewModel);

        Task SaveChangesAsync();

        Task<bool> IsLessonUnlocked(string categoryId,int lessonLevel, string studentId);
    }
}
