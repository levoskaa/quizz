using System.Collections.Generic;

namespace Quizz.GameService.Application.Models
{
    public class TypeInAnswerQuestion : Question
    {
        public override QuestionType Type => QuestionType.TypeInAnswer;

        private readonly List<string> acceptedAnswers;
        public IReadOnlyCollection<string> AcceptedAnswers => acceptedAnswers.AsReadOnly();

        public TypeInAnswerQuestion()
        {
            acceptedAnswers = new List<string>();
        }
    }
}