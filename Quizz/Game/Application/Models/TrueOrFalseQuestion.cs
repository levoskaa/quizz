namespace Quizz.GameService.Application.Models;

public class TrueOrFalseQuestion : Question
{
    public override QuestionType Type => QuestionType.TrueOrFalse;

    public bool CorrectAnswer { get; set; }
}
