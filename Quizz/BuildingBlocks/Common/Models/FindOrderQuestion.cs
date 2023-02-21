using System.Collections.Generic;

namespace Quizz.Common.Models;

public class FindOrderQuestion : Question
{
    public override QuestionType Type => QuestionType.FindOrder;

    public override IReadOnlyCollection<FindOrderAnswer> AnswerPossibilities
        => (IReadOnlyCollection<FindOrderAnswer>)answerPossibilites.AsReadOnly();

    public FindOrderQuestion(string text, int index, int timeLimitInSeconds)
        : base(text, index, timeLimitInSeconds)
    { }
}