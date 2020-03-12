using DigitalCoolBook.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services.Contracts
{
    public interface IQuestionService
    {
        IQueryable<Question> GetQuestions();

        Task<Question> GetQuestionAsync(string testId);

        Task SaveChangesAsync();

        Task AddQuestion(Question question);

        Task AddQuestionsAsync(List<Question> questions);

        IQueryable<Answer> GetAnswers();

        Task AddAnswersAsync(ICollection<Answer> answers);

        Task<Answer> GetAnswerAsync(string correctAnswerId);

        void RemoveRange(IQueryable<Answer> answersToRemove);
    }
}
