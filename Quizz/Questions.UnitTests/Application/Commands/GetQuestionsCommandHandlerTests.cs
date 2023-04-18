using Autofac.Extras.Moq;
using Moq;
using Quizz.Common.Models;
using Quizz.Questions.Application.Commands;
using Quizz.Questions.Data.Repositories;

namespace Questions.UnitTests.Application.Commands
{
    public class GetQuestionsCommandHandlerTests
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
            var command = GetSampleCommand();
            var questions = GetSampleQuestions();

            var mockRepository = mock.Mock<IQuestionRepository>();
            mockRepository.Setup(x => x.FilterAsync((question) => command.QuestionIds.Contains(question.Id)))
                .Returns(Task.FromResult(questions.AsEnumerable()));
            var commandHandler = mock.Create<GetQuestionsCommandHandler>();

            // Act
            var actual = (await commandHandler.Handle(command, default))
                .ToList();

            // Assert
            mockRepository.Verify(x => x.FilterAsync((question) => command.QuestionIds.Contains(question.Id)),
                Times.Exactly(1));
            Assert.Equal(command.QuestionIds.Count(), actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.Equal(questions[i].Id, actual[i].Id);
            }
        }

        public GetQuestionsCommand GetSampleCommand()
        {
            return new GetQuestionsCommand
            {
                QuestionIds = questionIds,
            };
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