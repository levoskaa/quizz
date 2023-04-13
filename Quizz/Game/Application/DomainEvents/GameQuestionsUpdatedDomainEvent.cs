using MediatR;
using Quizz.GameService.Application.Models;
using System;

namespace Quizz.GameService.Application.DomainEvents
{
    public class GameQuestionsUpdatedDomainEvent : INotification
    {
        public Game Game { get; init; }
        public DateTime UpdatedAt { get; init; }

        public GameQuestionsUpdatedDomainEvent(Game game, DateTime updatedAt)
        {
            Game = game;
            UpdatedAt = updatedAt;
        }
    }
}