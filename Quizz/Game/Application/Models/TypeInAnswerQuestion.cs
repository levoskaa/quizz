using System.Collections.Generic;

namespace Quizz.GameService.Application.Models
{
    public class TypeInAnswerQuestion : Question
    {
        public override QuestionType Type => QuestionType.TypeInAnswer;

        private readonly List<Answer> acceptedAnswers;
        public IReadOnlyCollection<Answer> AcceptedAnswers => acceptedAnswers.AsReadOnly();

        public TypeInAnswerQuestion()
        {
            acceptedAnswers = new List<Answer>();
        }
    }
}