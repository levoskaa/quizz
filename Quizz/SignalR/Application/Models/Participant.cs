namespace Quizz.SignalR.Application.Models
{
    public class Participant
    {
        public string Name { get; set; }
        public int Score { get; set; } = 0;
        public string SignalRConnectionId { get; set; }
    }
}
