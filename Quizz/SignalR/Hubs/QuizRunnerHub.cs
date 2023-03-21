using Microsoft.AspNetCore.SignalR;
using Quizz.SignalR.Application.Models;
using System.Threading.Tasks;

namespace Quizz.SignalR.Hubs
{
    public class QuizRunnerHub : Hub
    {
        public async Task JoinGroup(string inviteCode, ParticipantType participantType)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, inviteCode);
        }
    }
}