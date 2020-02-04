using DigitalCoolBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services.Contracts
{
    public interface IGradeService
    {
        IEnumerable<Grade> GetGrades();

        Task<Grade> GetGradeAsync(string id);
    }
}
