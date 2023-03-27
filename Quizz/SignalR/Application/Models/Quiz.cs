using Quizz.Common.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Quizz.SignalR.Application.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public int CurrentQuestionIndex { get; set; } = 0;
        public IEnumerable<Question> Questions { get; set; } = new List<Question>();
        private BlockingCollection<Participant> participants = new BlockingCollection<Participant>();
        public IEnumerable<Participant> Participants => participants;

        public void AddParticipant(Participant participant)
        {
            participants.Add(participant);
        }

        public Question GetCurrentQuestion()
        {
            // TODO: use the index property of the question instances to determine the current one
            return Questions.ElementAt(CurrentQuestionIndex);
        }

        public void ProgressToNextQuestion()
        {
            CurrentQuestionIndex++;
        }
    }
}