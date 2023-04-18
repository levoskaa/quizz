using Autofac.Extras.Moq;
using Moq;
using Quizz.Common.Models;
using Quizz.Questions.Application.IntegrationEvents;
using Quizz.Questions.Data.Repositories;
using System.Linq.Expressions;

namespace Questions.UnitTests.Application.IntegrationEvents
{
    public class GameDeletedIntegrationEventHandlerTests
    {
        private Guid[] questionIds = new[] {
            Guid.Parse("25fa9ef4-a1a3-43c6-ab86-118990d9bb99"),
            Guid.Parse("245d1ac4-e0e8-4fcf-9fe0-f03ff0610c60"),
            Guid.Parse("a7dc808a-85b4-4664-b9c2-d068bcd37fe0"),
        };

        [Fact]
        public async Task Handle_ShouldWork()
        {
            // Arrange
            using var mock = AutoMock.GetLoose();
            var @event = GetSampleEvent();
            var questions = GetSampleQuestions();

            var mockRepository = mock.Mock<IQuestionRepository>();
            mockRepository.Setup(x => x.FilterAsync(It.IsAny<Expression<Func<Question, bool>>>()))
                .Returns(Task.FromResult(questions.AsEnumerable()));
            mockRepository.Setup(x => x.Remove(It.IsAny<Question>()));
            mockRepository.Setup(x => x.UnitOfWork.SaveChangesAsync(default));
            var eventHandler = mock.Create<GameDeletedIntegrationEventHandler>();

            // Act
            await eventHandler.Handle(@event);

            // Assert
            mockRepository.Verify(x => x.FilterAsync(It.IsAny<Expression<Func<Question, bool>>>()),
                Times.Exactly(1));
            mockRepository.Verify(x => x.Remove(It.IsAny<Question>()), Times.Exactly(questions.Count));
            mockRepository.Verify(x => x.UnitOfWork.SaveChangesAsync(default), Times.Exactly(1));
        }

        public GameDeletedIntegrationEvent GetSampleEvent()
        {
            return new GameDeletedIntegrationEvent(questionIds);
        }

        public List<Question> GetSampleQuestions()
        {
            var questions = new List<Question>(new Question[] {
                new TrueOrFalseQuestion("", 0, 10, false)
                {
                    Id = questionIds[0],
                },
                new MultipleChoiceQuestion("", 1, 10)
                {
                    Id = questionIds[1],
                },
                new FreeTextQuestion("", 2, 10)
                {
                    Id = questionIds[2],
                }
            });
            return questions;
        }
    }
}