namespace DigitalCoolBook.Services.Contracts
{
    using DigitalCoolBook.Models;
    using System.Collections.Generic;
    using System.Linq;
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

        Task GetExpiredTest(string id);

        IQueryable<ExpiredTest> GetExpiredTests();
    }
}
