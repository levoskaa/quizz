using Quizz.Common.DataAccess;
using Quizz.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quizz.Questions.Data.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Guid Add(Question question);

        Task<IEnumerable<Question>> FilterAsync(Func<Question, bool> filter);

        void Remove(Question question);
    }
}