using System;

namespace Quizz.QuizRunner.Application.Models
{
    public class AnswerSubmission
    {
        public Guid QuestionId { get; set; }
        public bool Correct { get; set; }
        public dynamic Answer { get; set; }

        public AnswerSubmission(Guid questionId, bool correct, dynamic answer)
        {
            QuestionId = questionId;
            Correct = correct;
            Answer = answer;
        }
    }
}