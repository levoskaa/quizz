using System.ComponentModel.DataAnnotations;

namespace Quizz.Common.ViewModels;

public class AnswerViewModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Text { get; set; }

    [Required]
    public string QuestionId { get; set; }
}