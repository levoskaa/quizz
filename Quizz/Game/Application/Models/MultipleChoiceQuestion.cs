using System.Collections.Generic;

namespace Quizz.GameService.Application.Models
{
    public class MultipleChoiceQuestion : Question
    {
        public override QuestionType Type => QuestionType.MultipleChoice;

        private readonly List<Answer> answerPossibilites;
        public IReadOnlyCollection<Answer> AnswerPossibilities => answerPossibilites.AsReadOnly();

        private readonly List<int> correctAnswerIds;
        public IReadOnlyCollection<int> CorrectAnswerIds => correctAnswerIds.AsReadOnly();

        public MultipleChoiceQuestion()
        {
            answerPossibilites = new List<Answer>();
            correctAnswerIds = new List<int>();
        }
    }
}