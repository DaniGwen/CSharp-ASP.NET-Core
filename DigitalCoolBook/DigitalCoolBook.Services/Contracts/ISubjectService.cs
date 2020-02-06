using DigitalCoolBook.Models;
using System.Linq;

namespace DigitalCoolBook.Services.Contracts
{
    public interface ISubjectService
    {
        IQueryable<Subject> GetSubjects();
    }
}
