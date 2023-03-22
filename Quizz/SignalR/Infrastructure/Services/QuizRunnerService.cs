using Quizz.Common.Models;
using Quizz.SignalR.Application.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Quizz.SignalR.Infrastructure.Services
{
    public class QuizRunnerService : IQuizRunnerService
    {
        // Key: invite code
        // Value: quiz in progress
        private static readonly ConcurrentDictionary<string, Quiz> quizzes =
            new ConcurrentDictionary<string, Quiz>();

        public string InitQuiz(int quizId, IEnumerable<Question> questions)
        {
            string inviteCode;
            var newQuiz = new Quiz
            {
                Id = quizId,
                Questions = questions,
            };
            do
            {
                inviteCode = GenerateInviteCode(8);
            }
            // Keeps looping until a unique key is generated
            while (!quizzes.TryAdd(inviteCode, newQuiz));
            return inviteCode;
        }

        public bool QuizExists(string inviteCode)
        {
            return quizzes.ContainsKey(inviteCode);
        }

        private string GenerateInviteCode(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                // RandomNumberGenerator generates cryptographically strong random values
                var ch = chars[RandomNumberGenerator.GetInt32(chars.Length)];
                sb.Append(ch);
            }
            return sb.ToString();
        }
    }
}