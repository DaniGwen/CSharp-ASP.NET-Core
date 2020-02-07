using DigitalCoolBook.App.Data;
using DigitalCoolBook.Models;
using DigitalCoolBook.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task AddGradeParaleloAsync(GradeParalelo gradeParalelo)
        {
            await this.context.GradeParalelos.AddAsync(gradeParalelo);
        }

        public async Task<Grade> GetGradeAsync(string id)
        {
            return await this.context.Grades.FindAsync(id);
        }

        public async Task<GradeParalelo> GetGradeParaleloAsync(string id)
        {
            return await this.context.GradeParalelos.FindAsync(id);
        }

        public IQueryable<GradeParalelo> GetGradeParalelos()
        {
            return this.context.GradeParalelos;
        }

        public IQueryable<Grade> GetGrades()
        {
            return this.context.Grades;
        }

        public async Task RemoveGradeParaleloAsync(GradeParalelo paralelo)
        {
            this.context.GradeParalelos.Remove(paralelo);
            await this.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
