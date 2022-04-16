using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quizz.Common.ViewModels;

public class QuestionsListViewModel
{
    [Required]
    public IEnumerable<QuestionViewModel> Data { get; init; }
}