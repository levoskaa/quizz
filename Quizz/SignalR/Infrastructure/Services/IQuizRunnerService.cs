using Quizz.Common.Models;
using System.Collections.Generic;

namespace Quizz.SignalR.Infrastructure.Services
{
    public interface IQuizRunnerService
    {
        string InitQuiz(int quizId, IEnumerable<Question> questions);
        bool QuizExists(string inviteCode);
        void AddParticipant(string inviteCode, string name, string connectionId);
    }
}