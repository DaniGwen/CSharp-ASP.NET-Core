using DigitalCoolBook.App.Data;
using DigitalCoolBook.Models;
using DigitalCoolBook.Services.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services
{
    public class GradeService : IGradeService
    {
        private readonly ApplicationDbContext context;

        public GradeService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddGradeParaleloAsync(GradeTeacher gradeParalelo)
        {
            await this.context.GradeTeachers.AddAsync(gradeParalelo);
        }

        public async Task<Grade> GetGradeAsync(string id)
        {
            return await this.context.Grades.FindAsync(id);
        }

        public async Task<GradeTeacher> GetGradeParaleloAsync(string id)
        {
            return await this.context.GradeTeachers.FindAsync(id);
        }

        public IQueryable<GradeTeacher> GetGradeParalelos()
        {
            return this.context.GradeTeachers;
        }

        public IQueryable<Grade> GetGrades()
        {
            return this.context.Grades;
        }

        public async Task RemoveGradeParaleloAsync(GradeTeacher paralelo)
        {
            this.context.GradeTeachers.Remove(paralelo);
            await this.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
