namespace Quizz.Common.Models;

public class FreeTextQuestion : Question
{
    public override QuestionType Type => QuestionType.FreeText;

    public FreeTextQuestion(string text, int index, int timeLimitInSeconds)
        : base(text, index, timeLimitInSeconds)
    { }
}