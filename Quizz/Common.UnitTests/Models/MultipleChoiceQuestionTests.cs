using Quizz.Common.Models;
using System.Text.Json;

namespace Common.UnitTests.Models
{
    public class MultipleChoiceQuestionTests
    {
        public static IEnumerable<Answer> GetSampleAnswers()
        {
            return new Answer[]
            {
                new MultipleChoiceAnswer
                {
                    Id = 101,
                    IsCorrect = true
                },
                new MultipleChoiceAnswer
                {
                    Id = 111,
                    IsCorrect = true
                },
                new MultipleChoiceAnswer
                {
                    Id = 123,
                    IsCorrect = false
                },
                new MultipleChoiceAnswer
                {
                    Id = 201,
                    IsCorrect = false
                },
            };
        }

        public static IEnumerable<object[]> GetCheckAnswerData()
        {
            return new[] {
                // All correct but no incorrect answers
                new object[] {@"{""value"": [101, 111]}", true},
                // All correct and also some incorrect answers
                new object[] {@"{""value"": [101, 111, 201]}", false},
                // Some correct answers but not all of them
                new object[] {@"{""value"": [111]}", false},
                // Only incorrect answers
                new object[] {@"{""value"": [123]}", false},
                // No answers
                new object[] {@"{""value"": []}", false},
            };
        }

        [Theory]
        [MemberData(nameof(GetCheckAnswerData))]
        public void CheckAnswer_ShouldWork(string answerJson, bool expected)
        {
            // Arrange
            var question = new MultipleChoiceQuestion("", 0, 10);
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