using Quizz.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace Quizz.Common.ViewModels;

public class QuestionViewModel
{
    [Required]
    public string Id { get; init; }
    [Required]
    public string Text { get; init; }
    [Required]
    public QuestionType Type { get; init; }
    [Required]
    public int Index { get; init; }
    [Required]
    public int TimeLimitInSeconds { get; init; }
    public Answer AnswerPossibilites { get; init; }
    public int CorrectIdOrder { get; init; }
    public int CorrectAnswerIds { get; init; }
    public bool CorrectAnswer { get; init; }
    public Answer AcceptedAnswers { get; init; }
}