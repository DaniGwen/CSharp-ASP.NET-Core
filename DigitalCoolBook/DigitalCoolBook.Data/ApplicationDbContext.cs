using DigitalCoolBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalCoolBook.App.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<GradeParalelo> GradeParalelos { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<ScoreRecord> ScoreRecords { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<SubjectGrade> SubjectGrades { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<ExpiredTest> ExpiredTests { get; set; }

        public DbSet<CorrectAnswer> CorrectAnswers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasMany(c => c.Lessons)
                .WithOne(c => c.Category)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TestStudent>()
                .HasKey(k => new { k.StudentId, k.TestId });

            builder.Entity<Answer>().HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Test>().HasMany(test => test.Questions)
                .WithOne(question => question.Test)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<Question>().HasMany(test => test.Answers)
            //    .WithOne(question => question.Question)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
