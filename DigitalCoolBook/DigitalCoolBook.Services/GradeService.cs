using DigitalCoolBook.App.Data;
using DigitalCoolBook.Models;
using DigitalCoolBook.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services
{
    public class GradeService : IGradeService
    {
        private readonly ApplicationDbContext _context;

        public GradeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Grade> GetGradeAsync(string id)
        {
            var grade = await _context.Grades.FindAsync(id);

            return grade;
        }

        public IEnumerable<Grade> GetGrades()
        {
            var grades = _context.Grades.OrderBy(g => g.Name).ToList();

            return grades;
        }
    }
}
