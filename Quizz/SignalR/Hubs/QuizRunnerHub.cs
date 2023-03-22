using Microsoft.AspNetCore.SignalR;
using Quizz.SignalR.Application.Models;
using Quizz.SignalR.Infrastructure.Services;
using System.Threading.Tasks;

namespace Quizz.SignalR.Hubs
{
    public class QuizRunnerHub : Hub
    {
        private readonly IQuizRunnerService quizRunner;

        public QuizRunnerHub(IQuizRunnerService quizRunner)
        {
            this.quizRunner = quizRunner;
        }

        public async Task<bool> TryJoin(string inviteCode, ParticipantType participantType)
        {
            bool canBeAdded = participantType == ParticipantType.Owner ||
                (participantType == ParticipantType.Player && quizRunner.QuizExists(inviteCode));
            if (canBeAdded)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, inviteCode);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task SetPlayerName(string inviteCode, string name)
        {
            quizRunner.AddParticipant(inviteCode, name, Context.ConnectionId);
            await Clients.Group(inviteCode).SendAsync("PlayerJoined", name);
        }
    }
}