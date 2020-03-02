using DigitalCoolBook.App.Data;
using DigitalCoolBook.Models;
using DigitalCoolBook.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext context;

        public QuestionService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Task AddQuestion(Question question)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddQuestionsAsync(List<Question> questions)
        {
            await this.context.AddRangeAsync(questions);
            await this.SaveChangesAsync();
        }

        public Task CreateQuestion()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Answer> GetAnswers()
        {
            return this.context.Answers;
        }

        public async Task<Question> GetQuestionAsync(string testId)
        {
           return await this.context.Questions.FindAsync(testId);
        }

        public IQueryable<Question> GetQuestions()
        {
            return this.context.Questions;
        }

        public async Task SaveChangesAsync()
        {
           await this.context.SaveChangesAsync();
        }
    }
}
