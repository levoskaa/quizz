using Quizz.Common.DataAccess;

namespace Quizz.GameService.Application.Models
{
    public class Answer : Entity<int>
    {
        public string Text { get; set; }

        public string QuestionId { get; set; }
    }
}