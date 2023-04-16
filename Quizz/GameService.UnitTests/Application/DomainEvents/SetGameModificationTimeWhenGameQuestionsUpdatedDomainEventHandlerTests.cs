using Quizz.GameService.Application.DomainEvents;
using Quizz.GameService.Application.Models;

namespace GameService.UnitTests.Application.DomainEvents
{
    public class SetGameModificationTimeWhenGameQuestionsUpdatedDomainEventHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldWork()
        {
            // Arrange
            var game = GetSampleGame();
            var notification = GetSampleNotification(game);
            var eventHandler = new SetGameModificationTimeWhenGameQuestionsUpdatedDomainEventHandler();

            // Act
            await eventHandler.Handle(notification, default);

            // Assert
            Assert.Equal(notification.UpdatedAt, game.UpdatedAt);
        }

        private Game GetSampleGame()
        {
            return new Game
            {
                UpdatedAt = new DateTime(2000, 2, 3, 11, 21, 9),
            };
        }

        private GameQuestionsUpdatedDomainEvent GetSampleNotification(Game game)
        {
            var updateTime = new DateTime(2019, 9, 4, 21, 49, 12);
            return new GameQuestionsUpdatedDomainEvent(game, updateTime);
        }
    }
}