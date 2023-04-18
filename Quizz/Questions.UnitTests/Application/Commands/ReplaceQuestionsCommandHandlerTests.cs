using Autofac.Extras.Moq;
using Moq;
using Quizz.Common.Dtos;
using Quizz.Common.Models;
using Quizz.Questions.Application.Commands;
using Quizz.Questions.Data.Repositories;
using Quizz.Questions.Infrastructure.Exceptions;

namespace Questions.UnitTests.Application.Commands
{
    public class ReplaceQuestionsCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldWork()
        {
            // Arrange
            using var mock = AutoMock.GetLoose();
            var command = GetSampleCommand();
            var questionIds = command.QuestionIds;
            var oldQuestions = GetSampleOldQuestions();
            var mockRepository = mock.Mock<IQuestionRepository>();
            mockRepository.Setup(x => x.FilterAsync(question => questionIds.Contains(question.Id.ToString())))
                .Returns(Task.FromResult(oldQuestions));
            foreach (var question in oldQuestions)
            {
                mockRepository.Setup(x => x.Remove(question));
            }
            mockRepository.Setup(x => x.Add(It.IsAny<Question>()));
            mockRepository.Setup(x => x.UnitOfWork.SaveEntitiesAsync(default));
            var commandHandler = mock.Create<ReplaceQuestionsCommandHandler>();

            // Act
            var actual = await commandHandler.Handle(command, default);

            // Assert
            mockRepository.Verify(x => x.FilterAsync(question => questionIds.Contains(question.Id.ToString())),
                Times.Exactly(1));
            foreach (var question in oldQuestions)
            {
                mockRepository.Verify(x => x.Remove(question), Times.Exactly(1));
            }
            mockRepository.Verify(x => x.Add(It.IsAny<Question>()),
                Times.Exactly(command.QuestionDtos.Count()));
            mockRepository.Verify(x => x.UnitOfWork.SaveEntitiesAsync(default), Times.Exactly(1));
            Assert.Equal(command.QuestionDtos.Count(), actual.Count());
        }

        [Fact]
        public async Task Handle_ShouldNeverThrow()
        {
            // Arrange
            using var mock = AutoMock.GetLoose();
            var command = GetSampleCommand();
            var questionIds = command.QuestionIds;
            var oldQuestions = GetSampleOldQuestions();
            var mockRepository = mock.Mock<IQuestionRepository>();
            mockRepository.Setup(x => x.FilterAsync(question => questionIds.Contains(question.Id.ToString())))
                .Returns(Task.FromResult(oldQuestions));
            var commandHandler = mock.Create<ReplaceQuestionsCommandHandler>();

            // Act
            var exception = await Record.ExceptionAsync(() => commandHandler.Handle(command, default));

            // Assert
            var pass = exception is not QuestionsDomainException
                || exception.Message != "Question could not be created";
            Assert.True(pass);
        }

        private ReplaceQuestionsCommand GetSampleCommand()
        {
            return new ReplaceQuestionsCommand
            {
                QuestionIds = new[] {
                    "25fa9ef4-a1a3-43c6-ab86-118990d9bb99",
                    "245d1ac4-e0e8-4fcf-9fe0-f03ff0610c60",
                    "a7dc808a-85b4-4664-b9c2-d068bcd37fe0",
                },
                QuestionDtos = new[]
                {
                    new QuestionDto
                    {
                        Index = 0,
                        Text = "Q1",
                        Type = QuestionType.FreeText,
                        TimeLimitInSeconds = 10,
                    },
                    new QuestionDto
                    {
                        Index = 1,
                        Text = "Q2",
                        Type = QuestionType.MultipleChoice,
                        TimeLimitInSeconds = 10,
                    },
                    new QuestionDto
                    {
                        Index = 2,
                        Text = "Q3",
                        Type = QuestionType.TrueOrFalse,
                        TimeLimitInSeconds = 20,
                        CorrectAnswer = true,
                    },
                    new QuestionDto
                    {
                        Index = 3,
                        Text = "Q4",
                        Type = QuestionType.FindOrder,
                        TimeLimitInSeconds = 30,
                    },
                }
            };
        }

        private IEnumerable<Question> GetSampleOldQuestions()
        {
            var questions = new List<Question>(new Question[] {
                new TrueOrFalseQuestion("", 0, 10, false),
                new MultipleChoiceQuestion("", 1, 10),
                new FreeTextQuestion("", 2, 10),
            });
            return questions;
        }
    }
}