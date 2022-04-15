using System.Collections.Generic;

namespace Quizz.Common.ViewModels;

public class QuestionsListViewModel
{
    public IEnumerable<QuestionViewModel> Data { get; init; }
}