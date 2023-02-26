namespace Quizz.Common.Dtos;

public class AnswerDto
{
    public string Text { get; init; }
    public bool IsCorrect { get; init; }
    public int DisplayIndex { get; init; }
    public int CorrectIndex { get; init; }
}