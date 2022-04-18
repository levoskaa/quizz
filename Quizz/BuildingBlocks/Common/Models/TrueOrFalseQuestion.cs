namespace Quizz.Common.Models;

public class TrueOrFalseQuestion : Question
{
    public override QuestionType Type => QuestionType.TrueOrFalse;

    public TrueOrFalseQuestion(string text, int index, int timeLimitInSeconds)
        : base(text, index, timeLimitInSeconds)
    { }
}