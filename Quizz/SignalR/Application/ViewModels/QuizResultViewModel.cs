using System.Collections.Generic;

namespace Quizz.SignalR.Application.ViewModels
{
    public class QuizResultsViewModel
    {
        public IEnumerable<ParticipantResultViewModel> ParticipantResults { get; set; }
    }
}