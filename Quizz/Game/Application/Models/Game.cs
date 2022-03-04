using Quizz.Common.DataAccess;

namespace Quizz.GameService.Application.Models
{
    public class Game : IAggregateRoot
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string OwnerId { get; set; }
    }
}