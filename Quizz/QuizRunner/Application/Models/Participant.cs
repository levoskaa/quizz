using System.Collections.Generic;
using System.Linq;

namespace Quizz.QuizRunner.Application.Models
{
    public class Participant
    {
        public string Name { get; set; }
        public int Score => answerSubmissions.FindAll(submission => submission.Correct).Count();
        public string SignalRConnectionId { get; set; }
        private readonly List<AnswerSubmission> answerSubmissions = new();
        public IEnumerable<AnswerSubmission> AnswerSubmissions => answerSubmissions;

        public void AddAnswerSubmission(AnswerSubmission answerSubmission)
        {
            if (!answerSubmissions.Exists(prevSubmission => answerSubmission.QuestionId.Equals(prevSubmission.QuestionId)))
            {
                // Participant hasn't submitted an answer for this question yet
                answerSubmissions.Add(answerSubmission);
            }
        }

        public ParticipantResult GetResult()
        {
            return new ParticipantResult
            {
                Name = Name,
                ConnectionId = SignalRConnectionId,
                Score = Score,
            };
        }
    }
}