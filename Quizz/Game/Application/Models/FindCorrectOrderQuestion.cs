using System.Collections.Generic;

namespace Quizz.GameService.Application.Models
{
    public class FindCorrectOrderQuestion : Question
    {
        public override QuestionType Type => QuestionType.FindCorrectOrder;

        private readonly List<Answer> answerPossibilites;
        public IReadOnlyCollection<Answer> AnswerPossibilities => answerPossibilites.AsReadOnly();

        private readonly List<int> correctIdOrder;
        public IReadOnlyCollection<int> CorrectIdOrder => correctIdOrder.AsReadOnly();

        public FindCorrectOrderQuestion()
        {
            answerPossibilites = new List<Answer>();
            correctIdOrder = new List<int>();
        }
    }
}