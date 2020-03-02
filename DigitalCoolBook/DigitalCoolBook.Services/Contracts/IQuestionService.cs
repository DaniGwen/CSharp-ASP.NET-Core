﻿using DigitalCoolBook.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.Services.Contracts
{
    public interface IQuestionService
    {
        IQueryable GetQuestions();

        Task<Question> GetQuestionAsync(string testId);

        Task SaveChangesAsync();

        Task AddQuestion(Question question);

        Task AddQuestionsAsync(List<Question> questions);
    }
}
