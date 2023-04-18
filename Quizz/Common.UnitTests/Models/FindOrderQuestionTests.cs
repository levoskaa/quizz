using Quizz.Common.Models;
using System.Text.Json;

namespace Common.UnitTests.Models
{
    public class FindOrderQuestionTests
    {
        public static IEnumerable<object[]> GetCheckAnswerData()
        {
            return new[]
            {
                new object[] {@"{""value"": [22, 38, 19, 8]}", true},
                new object[] {@"{""value"": [38, 19, 22, 8]}", false},
            };
        }

        public static IEnumerable<Answer> GetSampleAnswers()
        {
            return new Answer[]
            {
                new FindOrderAnswer
                {
                    Id = 22,
                    CorrectIndex = 1,
                },
                new FindOrderAnswer
                {
                    Id = 38,
                    CorrectIndex = 2,
                },
                new FindOrderAnswer
                {
                    Id = 19,
                    CorrectIndex = 3,
                },
                new FindOrderAnswer
                {
                    Id = 8,
                    CorrectIndex = 4,
                },
            };
        }

        [Theory]
        [MemberData(nameof(GetCheckAnswerData))]
        public void CheckAnswer_ShouldWork(string answerJson, bool expected)
        {
            // Arrange
            var question = new FindOrderQuestion("", 0, 10);
            var answerPossibilities = GetSampleAnswers();
            question.ReplaceAnswerPossibilities(answerPossibilities);
            var answer = JsonSerializer.Deserialize<JsonElement>(answerJson)
                .GetProperty("value");

            // Act
            var actual = question.CheckAnswer(answer);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}