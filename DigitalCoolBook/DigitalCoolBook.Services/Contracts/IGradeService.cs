using DigitalCoolBook.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services.Contracts
{
    public interface IGradeService
    {
        IQueryable<Grade> GetGrades();

        Task<Grade> GetGradeAsync(string id);

        IQueryable<GradeParalelo> GetGradeParalelos();

        Task AddGradeParaleloAsync(GradeParalelo gradeParalelo);

        Task<GradeParalelo> GetGradeParaleloAsync(string id);

        Task SaveChangesAsync();
    }
}
