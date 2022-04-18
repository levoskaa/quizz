using Quizz.Common.Models;

namespace Quizz.Common.Dtos;

public class QuestionDto
{
    public string Text { get; init; }
    public QuestionType Type { get; init; }
    public int Index { get; init; }
    public int TimeLimitInSeconds { get; init; }
    public AnswerDto AnswerPossibilites { get; init; }
}