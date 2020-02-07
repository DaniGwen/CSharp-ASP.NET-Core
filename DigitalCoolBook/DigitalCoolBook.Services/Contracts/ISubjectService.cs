namespace DigitalCoolBook.Services.Contracts
{
    using DigitalCoolBook.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ISubjectService
    {
        IQueryable<Subject> GetSubjects();

        Task<Subject> GetSubjectAsync(string id);

        IQueryable<Lesson> GetLessons();
    }
}
