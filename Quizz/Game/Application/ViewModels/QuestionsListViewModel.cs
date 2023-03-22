using Quizz.Common.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quizz.GameService.Application.ViewModels;

public class QuestionsListViewModel
{
    [Required]
    public IEnumerable<QuestionViewModel> Data { get; init; }
}