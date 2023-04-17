using System.Collections.Generic;

namespace Quizz.QuizRunner.Application.ViewModels
{
    public class QuizResultsViewModel
    {
        public IEnumerable<ParticipantResultViewModel> ParticipantResults { get; set; }
    }
}