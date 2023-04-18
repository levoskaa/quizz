using Quizz.QuizRunner.Application.Models;

namespace QuizRunner.UnitTests.Application.Models
{
    public class ParticipantTests
    {
        [Fact]
        public void AddAnswerSubmission_AddingFirstSubmissionShouldWork()
        {
            // Arrange
            var participant = new Participant();
            var submissions = GetUniqueSubmissions();

            // Act
            foreach (var submission in submissions)
            {
                participant.AddAnswerSubmission(submission);
            }

            // Assert
            Assert.Equal(submissions.Count(), participant.AnswerSubmissions.Count());
            foreach (var submission in submissions)
            {
                Assert.Contains(submission, participant.AnswerSubmissions);
            }
        }

        [Fact]
        public void AddAnswerSubmission_SubmissionsAfterFirstShouldBeIgnored()
        {
            // Arrange
            var participant = new Participant();
            var submissions = GetRepeatedSubmissions();

            // Act
            foreach (var submission in submissions)
            {
                participant.AddAnswerSubmission(submission);
            }

            // Assert
            var expectedLength = 2;
            Assert.Equal(expectedLength, participant.AnswerSubmissions.Count());
            var acceptedQuestionIds = participant.AnswerSubmissions.Select(x => x.QuestionId);
            Assert.Distinct(acceptedQuestionIds);
        }

        [Fact]
        public void GetResult_ShouldWork()
        {
            // Arrange
            var participant = new Participant
            {
                Name = "P1",
                SignalRConnectionId = "id11",
            };
            var submissions = GetUniqueSubmissions();
            foreach (var submission in submissions)
            {
                participant.AddAnswerSubmission(submission);
            }
            var expected = new ParticipantResult
            {
                Name = "P1",
                ConnectionId = "id11",
                Score = submissions.FindAll(x => x.Correct).Count,
            };

            // Act
            var actual = participant.GetResult();

            // Assert
            bool isEqual = expected.Name == actual.Name
                && expected.ConnectionId == actual.ConnectionId
                && expected.Score == actual.Score;
            Assert.True(isEqual);
        }

        private List<AnswerSubmission> GetUniqueSubmissions()
        {
            return new List<AnswerSubmission>(new[] {
                new AnswerSubmission(Guid.Parse("073ac02d-6ca0-483b-86f3-feee0a1babdc"), false, null),
                new AnswerSubmission(Guid.Parse("ec1eedfa-1d79-40fe-87b3-23cbc7864dee"), true, null),
                new AnswerSubmission(Guid.Parse("0ece47a0-5012-42ca-b708-21705b8cd32b"), true, null),
                new AnswerSubmission(Guid.Parse("4b620c70-caad-4f0a-952e-8d0f8015918d"), false, null),
            });
        }

        private IEnumerable<AnswerSubmission> GetRepeatedSubmissions()
        {
            var repeatedId = Guid.Parse("073ac02d-6ca0-483b-86f3-feee0a1babdc");
            return new[] {
                new AnswerSubmission(repeatedId, false, null),
                new AnswerSubmission(Guid.Parse("ec1eedfa-1d79-40fe-87b3-23cbc7864dee"), true, null),
                new AnswerSubmission(repeatedId, true, null),
                new AnswerSubmission(repeatedId, false, null),
            };
        }
    }
}