namespace Quizz.Common.Models;

public class FindCorrectOrderQuestion : Question
{
    public override QuestionType Type => QuestionType.FindCorrectOrder;

    public FindCorrectOrderQuestion(string text, int index, int timeLimitInSeconds)
        : base(text, index, timeLimitInSeconds)
    { }
}