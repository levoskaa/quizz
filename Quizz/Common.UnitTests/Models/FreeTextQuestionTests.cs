using Quizz.Common.Models;
using System.Text.Json;

namespace Common.UnitTests.Models
{
    public class FreeTextQuestionTests
    {
        public static IEnumerable<object[]> GetCheckAnswerData()
        {
            return new[]
            {
                new object[] { @"{""value"": ""test answer""}", true},
                new object[] { @"{""value"": ""Test Answer""}", true},
                new object[] { @"{""value"": ""TEST ANSWER""}", true},
                new object[] { @"{""value"": ""some other answer""}", false},
                new object[] { @"{""value"": """"}", false},
            };
        }

        public static IEnumerable<Answer> GetSampleAnswers()
        {
            return new Answer[]
            {
                new Answer
                {
                    Text = "test answer",
                },
                new Answer
                {
                    Text = "Test Answer",
                },
                new Answer
                {
                    Text = "TEST ANSWER",
                },
            };
        }

        [Theory]
        [MemberData(nameof(GetCheckAnswerData))]
        public void CheckAnswer_ShouldWork(string answerJson, bool expected)
        {
            // Arrange
            var question = new FreeTextQuestion("", 0, 10);
            var correctAnswers = GetSampleAnswers();
            question.ReplaceAnswerPossibilities(correctAnswers);
            var answer = JsonSerializer.Deserialize<JsonElement>(answerJson)
                .GetProperty("value");

            // Act
            var actual = question.CheckAnswer(answer);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}