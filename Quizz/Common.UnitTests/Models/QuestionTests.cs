using Quizz.Common.Models;

namespace Common.UnitTests.Models
{
    public class QuestionTests
    {
        [Fact]
        public void ReplaceAnswerPossibilities_ShouldWork()
        {
            // Arrange
            var question = new FindOrderQuestion("", 0, 10);
            var oldAnswers = GetSampleAnswers();
            var newAnswers = GetSampleAnswers();

            // Act
            question.ReplaceAnswerPossibilities(oldAnswers);
            question.ReplaceAnswerPossibilities(newAnswers);

            // Assert
            Assert.Equal(newAnswers.Count, question.AnswerPossibilities.Count);
            foreach (var answer in newAnswers)
            {
                Assert.Contains(answer, question.AnswerPossibilities);
            }
        }

        private List<Answer> GetSampleAnswers()
        {
            var answers = new List<Answer>();
            var rnd = new Random();
            int count = rnd.Next(1, 10);
            for (int i = 0; i < count; i++)
            {
                var answer = new Answer();
                answers.Add(answer);
            }
            return answers;
        }
    }
}