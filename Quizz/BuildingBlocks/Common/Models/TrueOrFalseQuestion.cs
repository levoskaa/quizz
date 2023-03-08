namespace Quizz.Common.Models;

public class TrueOrFalseQuestion : Question
{
    public override QuestionType Type => QuestionType.TrueOrFalse;

    public bool CorrectAnswer { get; set; }

    public TrueOrFalseQuestion(string text, int index, int timeLimitInSeconds, bool correctAnswer)
        : base(text, index, timeLimitInSeconds)
    {
        CorrectAnswer = correctAnswer;
    }
}