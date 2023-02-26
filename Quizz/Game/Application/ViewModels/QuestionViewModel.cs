using Quizz.Common.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quizz.GameService.Application.ViewModels;

public class QuestionViewModel
{
    [Required]
    public string Text { get; init; }

    [Required]
    public QuestionType Type { get; init; }

    [Required]
    public int Index { get; init; }

    [Required]
    public int TimeLimitInSeconds { get; init; }

    public bool? CorrectAnswer { get; init; }

    public IEnumerable<AnswerViewModel> AnswerPossibilities { get; init; }
}