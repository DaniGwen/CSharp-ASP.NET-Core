using DigitalCoolBook.App.Data;
using DigitalCoolBook.Models;
using DigitalCoolBook.Services.Contracts;
using System.Linq;

namespace DigitalCoolBook.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ApplicationDbContext context;

        public SubjectService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Subject> GetSubjects()
        {
            return this.context.Subjects;
        }
    }
}
