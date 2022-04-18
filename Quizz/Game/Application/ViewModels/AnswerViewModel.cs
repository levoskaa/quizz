using System.ComponentModel.DataAnnotations;

namespace Quizz.GameService.Application.ViewModels;

public class AnswerViewModel
{
    [Required]
    public string Text { get; set; }

    [Required]
    public bool IsCorrect { get; set; }

    [Required]
    public int Index { get; set; }
}