using Autofac.Extras.Moq;
using Moq;
using Quizz.Common.ErrorHandling;
using Quizz.GameService.Application.Commands;
using Quizz.GameService.Application.Models;
using Quizz.GameService.Data.Repositories;

namespace GameService.UnitTests.Application.Commands
{
    public class UpdateGameCommandHandlerTests
    {
        public static IEnumerable<object[]> DataForValidCommand =>
            new List<object[]>
            {
                new object[] {
                    new UpdateGameCommand
                    {
                        Name = "new name"
                    },
                    new Game
                    {
                        Name = "old name",
                        UpdatedAt = new DateTime(2009, 3, 1, 7, 0, 10),
                    },
                },
                new object[] {
                    new UpdateGameCommand
                    {
                        Name = "a very long, complicated name for this game"
                    },
                    new Game
                    {
                        Name = "some other name",
                        UpdatedAt = new DateTime(2023, 11, 23, 19, 30, 11),
                    },
                },
            };

        [Theory]
        [MemberData(nameof(DataForValidCommand))]
        public async Task Handle_ValidCommandShouldWork(UpdateGameCommand command, Game game)
        {
            // Arrange
            using var mock = AutoMock.GetLoose();

            var mockRepository = mock.Mock<IGameRepository>();
            mockRepository.Setup(x => x.GetAsync(command.GameId))
                .Returns(Task.FromResult(game));
            mockRepository.Setup(x => x.UnitOfWork.SaveEntitiesAsync(default));
            var commandHandler = mock.Create<UpdateGameCommandHandler>();

            // Act
            await commandHandler.Handle(command, default);

            // Assert
            mockRepository.Verify(x => x.GetAsync(command.GameId), Times.Exactly(1));
            mockRepository.Verify(x => x.UnitOfWork.SaveEntitiesAsync(default), Times.Exactly(1));
            Assert.Equal(command.Name, game.Name);
            Assert.True((DateTime.UtcNow - game.UpdatedAt) < TimeSpan.FromSeconds(2));
        }

        [Fact]
        public async Task Handle_InvalidCommandShouldThrow()
        {
            // Arrange
            using var mock = AutoMock.GetLoose();
            var game = GetSampleGame(23);
            var command = GetSampleCommand(23, "3c6e0579-3bf0-4e80-b3b0-cab4e475da35");

            var mockRepository = mock.Mock<IGameRepository>();
            mockRepository.Setup(x => x.GetAsync(game.Id))
                .Returns(Task.FromResult(game));
            mockRepository.Setup(x => x.UnitOfWork.SaveEntitiesAsync(default));
            var commandHandler = mock.Create<UpdateGameCommandHandler>();

            // Act + Assert
            await Assert.ThrowsAsync<ForbiddenException>(() => commandHandler.Handle(command, default));
            mockRepository.Verify(x => x.GetAsync(game.Id), Times.Exactly(1));
            mockRepository.Verify(x => x.UnitOfWork.SaveEntitiesAsync(default), Times.Never());
        }

        private UpdateGameCommand GetSampleCommand(int gameId = 11, string userId = "7269b01d-5284-4f8f-b7fe-7d1cf08f5aba")
        {
            return new UpdateGameCommand
            {
                GameId = gameId,
                UserId = userId,
            };
        }

        private Game GetSampleGame(int gameId = 11, string ownerId = "7269b01d-5284-4f8f-b7fe-7d1cf08f5aba")
        {
            return new Game
            {
                Id = gameId,
                OwnerId = ownerId,
            };
        }
    }
}