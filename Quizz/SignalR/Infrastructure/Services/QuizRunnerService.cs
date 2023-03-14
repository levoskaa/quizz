using Quizz.SignalR.Application.Models;
using System.Collections.Concurrent;

namespace Quizz.SignalR.Infrastructure.Services
{
    public class QuizRunnerService : IQuizRunnerService
    {
        // Key: invite code
        // Value: quiz in progress
        private static readonly ConcurrentDictionary<string, Quiz> quizes =
            new ConcurrentDictionary<string, Quiz>();

        public QuizRunnerService()
        {
        }
    }
}