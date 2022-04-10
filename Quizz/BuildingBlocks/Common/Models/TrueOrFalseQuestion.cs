namespace Quizz.Common.Models;

public class TrueOrFalseQuestion : Question
{
    public override QuestionType Type => QuestionType.TrueOrFalse;

    public bool CorrectAnswer { get; set; }
}
