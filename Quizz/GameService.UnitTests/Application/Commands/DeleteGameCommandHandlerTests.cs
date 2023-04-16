using Autofac.Extras.Moq;
using Moq;
using Quizz.Common.ErrorHandling;
using Quizz.GameService.Application.Commands;
using Quizz.GameService.Application.Models;
using Quizz.GameService.Data.Repositories;

namespace GameService.UnitTests.Application.Commands
{
    public class DeleteGameCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommandShouldWork()
        {
            // Arrange
            using var mock = AutoMock.GetLoose();
            var gameId = 5;
            var command = GetSampleCommand();
            var game = GetSampleGame();

            var mockRepository = mock.Mock<IGameRepository>();
            mockRepository.Setup(x => x.GetAsync(gameId))
                .Returns(Task.FromResult(game));
            mockRepository.Setup(x => x.Remove(game));
            mockRepository.Setup(x => x.UnitOfWork.SaveEntitiesAsync(default));
            var commandHandler = mock.Create<DeleteGameCommandHandler>();

            // Act
            await commandHandler.Handle(command, default);

            // Assert
            mockRepository.Verify(x => x.GetAsync(gameId), Times.Exactly(1));
            mockRepository.Verify(x => x.Remove(game), Times.Exactly(1));
            mockRepository.Verify(x => x.UnitOfWork.SaveEntitiesAsync(default), Times.Exactly(1));
        }

        [Fact]
        public async Task Handle_InvalidCommandShouldThrow()
        {
            // Arrange
            using var mock = AutoMock.GetLoose();
            var gameId = 121;
            var initiatorId = "31e2b1e1-0d67-47d2-bb24-834d71066089";
            var command = GetSampleCommand(gameId, initiatorId);
            var game = GetSampleGame();

            var mockRepository = mock.Mock<IGameRepository>();
            mockRepository.Setup(x => x.GetAsync(gameId))
                .Returns(Task.FromResult(game));
            mockRepository.Setup(x => x.Remove(game));
            mockRepository.Setup(x => x.UnitOfWork.SaveEntitiesAsync(default));
            var commandHandler = mock.Create<DeleteGameCommandHandler>();

            // Act + Assert
            await Assert.ThrowsAsync<ForbiddenException>(() => commandHandler.Handle(command, default));
            mockRepository.Verify(x => x.GetAsync(gameId), Times.Exactly(1));
            mockRepository.Verify(x => x.Remove(game), Times.Never());
            mockRepository.Verify(x => x.UnitOfWork.SaveEntitiesAsync(default), Times.Never());
        }

        public static DeleteGameCommand GetSampleCommand(int gameId = 5, string userId = "26554dbb-7de2-4306-8696-26cd8166bdbc")
        {
            return new DeleteGameCommand(gameId, userId);
        }

        public static Game GetSampleGame(int gameId = 5, string ownerId = "26554dbb-7de2-4306-8696-26cd8166bdbc")
        {
            return new Game
            {
                Id = gameId,
                OwnerId = ownerId,
            };
        }
    }
}