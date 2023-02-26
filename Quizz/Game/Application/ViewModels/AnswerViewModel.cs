using System.ComponentModel.DataAnnotations;

namespace Quizz.GameService.Application.ViewModels;

public class AnswerViewModel
{
    [Required]
    public string Text { get; set; }

    public bool? IsCorrect { get; set; }
    public int? CorrectIndex { get; set; }
    public int? DisplayIndex { get; set; }
}