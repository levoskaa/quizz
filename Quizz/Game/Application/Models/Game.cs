using Quizz.Common.DataAccess;
using System;

namespace Quizz.GameService.Application.Models
{
    public class Game : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public string OwnerId { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}