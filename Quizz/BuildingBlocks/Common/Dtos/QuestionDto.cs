using Quizz.Common.Models;
using System.Collections.Generic;

namespace Quizz.Common.Dtos;

public class QuestionDto
{
    public string Text { get; init; }
    public QuestionType Type { get; init; }
    public int Index { get; init; }
    public int TimeLimitInSeconds { get; init; }
    public bool? CorrectAnswer { get; init; }
    public IEnumerable<AnswerDto> AnswerPossibilities { get; init; } = new List<AnswerDto>();
}