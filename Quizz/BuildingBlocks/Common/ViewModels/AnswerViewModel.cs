using System.ComponentModel.DataAnnotations;

namespace Quizz.Common.ViewModels;

public class AnswerViewModel
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Text { get; set; }

    public bool? IsCorrect { get; set; }
    public int? CorrectIndex { get; set; }
    public int? DisplayIndex { get; set; }
}