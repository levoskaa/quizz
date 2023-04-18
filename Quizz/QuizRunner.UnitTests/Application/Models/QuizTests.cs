using Moq;
using Quizz.Common.Models;
using Quizz.QuizRunner.Application.Models;
using Quizz.QuizRunner.Infrastructure.Exceptions;

namespace QuizRunner.UnitTests.Application.Models
{
    public class QuizTests
    {
        [Fact]
        public void ProgressToNextQuestion_ShouldWork()
        {
            // Arrange
            var questions = GetSampleQuestions();
            var quiz = new Quiz
            {
                CurrentQuestionIndex = 1,
                Questions = questions,
            };
            var expectedIndex = 2;
            var expectedQuestion = questions[expectedIndex];

            // Act
            quiz.ProgressToNextQuestion();

            // Assert
            Assert.Equal(expectedIndex, quiz.CurrentQuestionIndex);
            Assert.Equal(expectedQuestion, quiz.CurrentQuestion);
        }

        [Fact]
        public void ProgressToNextQuestion_ShouldThrowOnLastQuestion()
        {
            // Arrange
            var questions = GetSampleQuestions();
            var quiz = new Quiz
            {
                CurrentQuestionIndex = questions.Count - 1,
                Questions = questions,
            };

            // Act + Assert
            Assert.Throws<NoQuestionsRemainingException>(() => quiz.ProgressToNextQuestion());
        }

        // TODO: test SubmitAnswer method for valid and late submissions

        [Fact]
        public void GetResults_ShouldWork()
        {
            // Arrange
            var quiz = new Quiz();
            var participants = GetSampleParticipants();
            foreach (var participant in participants)
            {
                quiz.AddParticipant(participant);
            }

            // Act
            var actual = quiz.GetResults().ToList();

            // Assert
            Assert.Equal(participants.Count, actual.Count);
            for (int i = 1; i < actual.Count; i++)
            {
                var currentScore = actual[i].Score;
                var previousScore = actual[i - 1].Score;
                // Check descending order
                Assert.True(previousScore >= currentScore);
            }
        }

        private List<Question> GetSampleQuestions()
        {
            return new List<Question>(new Question[]
            {
                new TrueOrFalseQuestion("Q1", 0, 10, true),
                new MultipleChoiceQuestion("Q2", 1, 15),
                new FindOrderQuestion("Q3", 2, 45),
                new FreeTextQuestion("Q4", 3, 15),
            });
        }

        private List<Participant> GetSampleParticipants()
        {
            var participants = new List<Participant>();
            for (int i = 0; i < 5; i++)
            {
                var result = new ParticipantResult
                {
                    Score = i,
                };
                var mockParticipant = new Mock<Participant>();
                mockParticipant.Setup(x => x.GetResult())
                    .Returns(result);
                participants.Add(mockParticipant.Object);
            }
            return participants;
        }
    }
}