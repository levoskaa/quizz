using Quizz.Common.DataAccess;
using System;

namespace Quizz.GameService.Application.Models
{
    public abstract class Question : Entity<Guid>
    {
        public string Text { get; set; }

        public abstract QuestionType Type { get; }

        public int Index { get; set; }

        public int TimeLimitInSeconds { get; set; }
    }
}