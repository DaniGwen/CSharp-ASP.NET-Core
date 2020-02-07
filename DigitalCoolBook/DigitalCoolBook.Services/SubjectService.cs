using DigitalCoolBook.App.Data;
using DigitalCoolBook.Models;
using DigitalCoolBook.Services.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ApplicationDbContext context;

        public SubjectService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Subject> GetSubjectAsync(string id)
        {
            return await context.Subjects.FindAsync(id);
        }

        public IQueryable<Subject> GetSubjects()
        {
            return this.context.Subjects;
        }
    }
}
