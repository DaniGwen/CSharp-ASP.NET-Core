namespace DigitalCoolBook.Services.Contracts
{
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Web.Models.TestviewModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface ITestService
    {
        Test GetTestByLesson(string id);

        Task SaveChangesAsync();

        Task AddTestAsync(Test test);

        Task AddTestStudentsAsync(List<TestStudent> testStudentList);

        IQueryable<Test> GetTests();

        Task<Test> GetTestAsync(string id);

        Task AddExpiredTestAsync(ExpiredTest expiredTest);

        Task RemoveTestAsync(string testId);

        Task AddTestStudentsAsync(ICollection<TestStudent> testStudents);

        Task<ExpiredTest> GetExpiredTest(string id);

        IQueryable<ExpiredTest> GetExpiredTests();

        Task RemoveExpiredTest(string expiredTestId, string studentId);

        Task<string> AddTestRoomAsync(string[] students, string teacherId, string testId);

        string IsStudentInTest(string studentId);

        bool CheckAllFinished();

        TestRoomStudent GetTestRoomStudent(string studentId);

        Task RemoveTestRoomAsync(string testId);

        List<Test> GetActiveTestsByTeacherId(string teacherId);

        Task<List<string>> GetStudentsInTestRoomAsync(string testId);

        List<TestExpiredViewModel> GetExpiredTestsByTeacherId(string teacherId);

        Task TestRoomStudentFinished(string studentId, int score);
    }
}
