namespace Quizz.GameService.Application.Models
{
    public class GameQuestion
    {
        public int GameId { get; set; }
        public Game Game { get; set; }

        public string QuestionId { get; set; }
        public Question Question { get; set; }
    }
}