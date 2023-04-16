using Autofac.Extras.Moq;
using AutoMapper;
using Moq;
using Quizz.GameService.Application.Commands;
using Quizz.GameService.Application.Models;
using Quizz.GameService.Data.Repositories;

namespace GameService.UnitTests.Application.Commands
{
    public class CreateGameCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldWork()
        {
            // Arrange
            using var mock = AutoMock.GetLoose();
            var command = GetSampleCommand();
            var game = GetSampleGame();

            mock.Mock<IMapper>()
                .Setup(x => x.Map<Game>(command))
                .Returns(game);
            var mockRepository = mock.Mock<IGameRepository>();
            mockRepository.Setup(x => x.Add(game))
                .Returns(11);
            mockRepository.Setup(x => x.UnitOfWork.SaveEntitiesAsync(default));
            var commandHandler = mock.Create<CreateGameCommandHandler>();
            var expected = 11;

            // Act
            var actual = await commandHandler.Handle(command, default);

            // Assert
            mockRepository.Verify(x => x.Add(game), Times.Exactly(1));
            mockRepository.Verify(x => x.UnitOfWork.SaveEntitiesAsync(default), Times.Exactly(1));
            Assert.Equal(expected, actual);
        }

        public static CreateGameCommand GetSampleCommand()
        {
            return new CreateGameCommand
            {
                Name = "Quiz on 2012 London Olympic Games",
                UserId = "6eac909c-7ff1-47cd-affd-209fa0619378",
            };
        }

        public static Game GetSampleGame()
        {
            return new Game
            {
                Name = "Quiz on 2012 London Olympic Games",
                OwnerId = "6eac909c-7ff1-47cd-affd-209fa0619378",
            };
        }
    }
}