using Quizz.Common.ViewModels;
using System.Collections.Generic;

namespace Quizz.SignalR.Application.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public int CurrentQuestionIndex { get; set; }
        public IEnumerable<QuestionViewModel> Questions { get; set; }
        public IEnumerable<Participant> Participants { get; set; }
    }
}