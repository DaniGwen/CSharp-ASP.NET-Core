using DigitalCoolBook.App.Data;
using DigitalCoolBook.Models;
using DigitalCoolBook.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DigitalCoolBook.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext context;

        public QuestionService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddAnswersAsync(ICollection<Answer> answers)
        {
            await this.context.Answers.AddRangeAsync(answers);
            await this.SaveChangesAsync();
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

        public async Task SetAnswerCorrectAsync(string correctAnswerId)
        {
            var correctAnswer =  await this.context.Answers
                .FirstOrDefaultAsync(x => x.AnswerId == correctAnswerId);

            if (correctAnswer != null)
            {
                correctAnswer.IsCorrect = true;
                await context.SaveChangesAsync();
            }
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

        public async Task RemoveAnswers(List<Answer> answersToRemove)
        {
            this.context.Answers.RemoveRange(answersToRemove);

            await this.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
           await this.context.SaveChangesAsync();
        }
    }
}
