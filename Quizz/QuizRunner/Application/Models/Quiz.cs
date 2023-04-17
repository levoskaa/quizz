using Quizz.Common.Models;
using Quizz.QuizRunner.Infrastructure.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Quizz.QuizRunner.Application.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public int CurrentQuestionIndex { get; set; } = -1;
        public Question CurrentQuestion =>
            questions.SingleOrDefault(question => question.Index == CurrentQuestionIndex);

        private readonly List<Question> questions = new();
        public IEnumerable<Question> Questions
        {
            get => questions;
            set
            {
                questions.Clear();
                questions.AddRange(value);
                questions.Sort((a, b) => a.Index - b.Index);
            }
        }

        private readonly BlockingCollection<Participant> participants = new();
        // Key: Question id, value: deadline for answering
        private readonly Dictionary<Guid, DateTime> answersDeadline = new();

        public void AddParticipant(Participant participant)
        {
            participants.Add(participant);
        }

        public void ProgressToNextQuestion()
        {
            CurrentQuestionIndex++;
            if (CurrentQuestionIndex == questions.Count)
            {
                throw new NoQuestionsRemainingException();
            }
            var deadline = DateTime.Now.AddSeconds(CurrentQuestion.TimeLimitInSeconds);
            answersDeadline.Add(CurrentQuestion.Id, deadline);
        }

        public bool SubmitAnswer(Guid questionId, string connectionId, JsonElement rawAnswer)
        {
            answersDeadline.TryGetValue(questionId, out DateTime deadline);
            if (DateTime.Now > deadline)
            {
                // Deadline has passed, ignore submission
                return false;
            }
            var answerCorrect = CurrentQuestion.CheckAnswer(rawAnswer);
            var answerSubmission = new AnswerSubmission(CurrentQuestion.Id, answerCorrect, rawAnswer);
            var participant = participants.Single(participant => participant.SignalRConnectionId == connectionId);
            participant.AddAnswerSubmission(answerSubmission);
            return answerCorrect;
        }

        public IEnumerable<ParticipantResult> GetResults()
        {
            return participants.Select(participant => participant.GetResult())
                .OrderByDescending(participant => participant.Score);
        }
    }
}