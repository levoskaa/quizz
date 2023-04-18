using Quizz.GameService.Application.Models;

namespace GameService.UnitTests.Application.Models
{
    public class GameTests
    {
        [Fact]
        public void ReplaceQuestionIds_ShouldWork()
        {
            // Arrange
            var game = new Game();
            var oldQuestionIds = GetSampleQuestionIds();
            var newQuestionIds = GetSampleQuestionIds();

            // Act
            game.ReplaceQuestionIds(oldQuestionIds);
            game.ReplaceQuestionIds(newQuestionIds);

            // Assert
            Assert.Equal(newQuestionIds.Count(), game.QuestionIds.Count());
            foreach (var questionId in newQuestionIds)
            {
                Assert.Contains(questionId, game.QuestionIds);
            }
        }

        private IEnumerable<Guid> GetSampleQuestionIds()
        {
            var questionIds = new List<Guid>();
            var random = new Random();
            var count = random.Next(1, 10);
            for (int i = 0; i < count; i++)
            {
                var guid = Guid.NewGuid();
                questionIds.Add(guid);
            }
            return questionIds;
        }
    }
}