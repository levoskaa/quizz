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
        private readonly List<Question> questions = new();
        public IEnumerable<Question> Questions {
            get => questions;
            set
            { 
                questions.Clear();
                questions.AddRange(value);
                questions.Sort((a, b) => a.Index - b.Index);
            }
        }
        private BlockingCollection<Participant> participants = new();
        public IEnumerable<Participant> Participants => participants;

        public void AddParticipant(Participant participant)
        {
            participants.Add(participant);
        }

        public Question GetCurrentQuestion()
        {
            return Questions.SingleOrDefault(question => question.Index == CurrentQuestionIndex);
        }

        public void ProgressToNextQuestion()
        {
            CurrentQuestionIndex++;
        }
    }
}