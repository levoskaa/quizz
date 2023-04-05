using System.Collections.Generic;
using System.Linq;

namespace Quizz.SignalR.Application.Models
{
    public class Participant
    {
        public string Name { get; set; }
        public int Score => AnswerSubmissions.Select(submission => submission.Correct).Count();
        public string SignalRConnectionId { get; set; }
        private readonly List<AnswerSubmission> answerSubmissions = new();
        public IEnumerable<AnswerSubmission> AnswerSubmissions => answerSubmissions;

        public void AddAnswerSubmission(AnswerSubmission answerSubmission)
        {
            if (!answerSubmissions.Exists(prevSubmission => answerSubmission.QuestionId.Equals(prevSubmission.QuestionId))) {
                // Participant hasn't submitted an answer for this question yet
                answerSubmissions.Add(answerSubmission);
            }
        }
    }
}