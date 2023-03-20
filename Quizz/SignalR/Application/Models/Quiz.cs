using Quizz.Common.Models;
using System.Collections.Generic;

namespace Quizz.SignalR.Application.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public int CurrentQuestionIndex { get; set; } = 0;
        public IEnumerable<Question> Questions { get; set; } = new List<Question>();
        public IEnumerable<Participant> Participants { get; set; } = new List<Participant>();
    }
}