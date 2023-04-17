using Quizz.Common.ViewModels;
using System.Collections.Generic;

namespace Quizz.QuizRunner.Application.ViewModels
{
    public class GameInitializedViewModel
    {
        public string InviteCode { get; set; }
        public IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}
