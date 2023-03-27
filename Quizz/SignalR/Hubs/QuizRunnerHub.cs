using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Quizz.Common.ViewModels;
using Quizz.SignalR.Application.Models;
using Quizz.SignalR.Infrastructure.Services;
using System.Threading.Tasks;

namespace Quizz.SignalR.Hubs
{
    public class QuizRunnerHub : Hub
    {
        private readonly IQuizRunnerService quizRunner;
        private readonly IMapper mapper;

        public QuizRunnerHub(IQuizRunnerService quizRunner, IMapper mapper)
        {
            this.quizRunner = quizRunner;
            this.mapper = mapper;
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

        public async Task StartGame(string inviteCode)
        {
            await Clients.Group(inviteCode).SendAsync("GameStarted");
        }

        public async Task GetCurrentQuestion(string inviteCode)
        {
            var question = quizRunner.GetCurrentQuestion(inviteCode);
            var questionViewModel = mapper.Map<QuestionViewModel>(question);
            await Clients.Group(inviteCode).SendAsync("QuestionReceived", questionViewModel);
        }

        public async Task ProgressToNextQuestion(string inviteCode)
        {
            quizRunner.ProgressToNextQuestion(inviteCode);
        }
    }
}