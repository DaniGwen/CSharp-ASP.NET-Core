using DigitalCoolBook.App.Data;
using DigitalCoolBook.Models;
using DigitalCoolBook.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services
{
    public class ScoreService : IScoreService
    {
        private readonly ApplicationDbContext _context;

        public ScoreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddScoreAsync(Score score)
        {
            await _context.Scores.AddAsync(score);
            await this.SaveChangesAsync();
        }

        public async Task AddScoreStudentAsync(ScoreStudent scoreStudent)
        {
            await _context.ScoreStudents.AddAsync(scoreStudent);
            await this.SaveChangesAsync();
        }

        public async Task<Score> GetScoreAsync(string scoreId)
        {
            return await _context.Scores.FindAsync(scoreId);
        }

        public IQueryable<Score> GetScores()
        {
            return _context.Scores;
        }

        public IQueryable<ScoreStudent> GetScoreStudents()
        {
            return _context.ScoreStudents
                .Include(x => x.Score)
                .ThenInclude(x => x.Lesson);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async void CreateScore(int points, string lessonId, string studentId)
        {
            var previousScore = _context.ScoreStudents
                .Include(x => x.Score)
                .FirstOrDefault(x => x.StudentId == studentId);

            var score = new Score
            {
                ScorePoints = points,
                LessonId = lessonId,
            };

            var scoreStudent = new ScoreStudent
            {
                ScoreId = score.ScoreId,
                StudentId = studentId,
            };

            if (previousScore != null)
            {
                if (previousScore.Score.ScorePoints < points)
                {
                    _context.ScoreStudents.Remove(previousScore);
                    _context.Scores.Remove(previousScore.Score);
                }
            }

            await _context.ScoreStudents.AddAsync(scoreStudent);
            await _context.Scores.AddAsync(score);
            await _context.SaveChangesAsync();
        }
    }
}
