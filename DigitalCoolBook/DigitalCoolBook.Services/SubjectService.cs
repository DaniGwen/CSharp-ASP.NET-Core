namespace DigitalCoolBook.Services
{
    using DigitalCoolBook.App.Data;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using System.Linq;
    using System.Threading.Tasks;

    public class SubjectService : ISubjectService
    {
        private readonly ApplicationDbContext context;

        public SubjectService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Category> GetCategories()
        {
            return this.context.Categories;
        }

        public async Task<Lesson> GetLessonAsync(string id)
        {
            return await this.context.Lessons.FindAsync(id);
        }

        public IQueryable<Lesson> GetLessons()
        {
            return this.context.Lessons;
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
