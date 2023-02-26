namespace Quizz.Common.Models;

public class FindOrderQuestion : Question
{
    public override QuestionType Type => QuestionType.FindOrder;

    public FindOrderQuestion(string text, int index, int timeLimitInSeconds)
        : base(text, index, timeLimitInSeconds)
    { }
}