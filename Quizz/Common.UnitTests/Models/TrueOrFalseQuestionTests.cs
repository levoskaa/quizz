using Quizz.Common.Models;
using System.Text.Json;

namespace Common.UnitTests.Models
{
    public class TrueOrFalseQuestionTests
    {
        public static IEnumerable<object[]> CheckAnswerData = new[]
        {
            new object[] { @"{""value"": true}", true, true },
            new object[] { @"{""value"": true}", false, false },
            new object[] { @"{""value"": false}", true, false },
            new object[] { @"{""value"": false}", false, true },
        };

        [Theory]
        [MemberData(nameof(CheckAnswerData))]
        public void CheckAnswer_ShouldWork(string answerJson, bool correctAnswer, bool expected)
        {
            // Arrange
            var answer = JsonSerializer.Deserialize<JsonElement>(answerJson)
                .GetProperty("value");
            var question = new TrueOrFalseQuestion("", 0, 10, correctAnswer);

            // Act
            var actual = question.CheckAnswer(answer);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}