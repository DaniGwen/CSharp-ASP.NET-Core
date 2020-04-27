using DigitalCoolBook.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services.Contracts
{
    public interface IGradeService
    {
        IQueryable<Grade> GetGrades();

        Task<Grade> GetGradeAsync(string id);

        IQueryable<GradeTeacher> GetGradeParalelos();

        Task AddGradeParaleloAsync(GradeTeacher gradeParalelo);

        Task<GradeTeacher> GetGradeParaleloAsync(string id);

        Task RemoveGradeParaleloAsync(GradeTeacher paralelo);
        Task SaveChangesAsync();
    }
}
