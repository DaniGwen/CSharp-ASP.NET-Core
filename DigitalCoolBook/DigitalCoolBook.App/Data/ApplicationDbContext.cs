using DigitalCoolBook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalCoolBook.App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<GradeParalelo> GradeParalelos { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<ScoreRecord> ScoreRecords { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<SubjectGrade> SubjectGrades { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }

    public class Attendance
    {
    }
}
