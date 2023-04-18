using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Quizz.Common.ViewModels;
using Quizz.QuizRunner.Application.Models;
using Quizz.QuizRunner.Application.ViewModels;
using Quizz.QuizRunner.Infrastructure.Exceptions;
using Quizz.QuizRunner.Infrastructure.Services;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Quizz.QuizRunner.Hubs
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
            try
            {
                quizRunner.ProgressToNextQuestion(inviteCode);
            }
            catch (NoQuestionsRemainingException)
            {
                await Clients.Group(inviteCode).SendAsync("QuizOver");
            }
        }

        public bool AnswerQuestion(string inviteCode, string questionId, JsonElement rawAnswer)
        {
            return quizRunner.SubmitAnswer(inviteCode, Guid.Parse(questionId), Context.ConnectionId, rawAnswer.GetProperty("value"));
        }

        public async Task DisplayResults(string inviteCode)
        {
            await Clients.Group(inviteCode).SendAsync("DisplayResults");
        }

        public QuizResultsViewModel GetQuizResults(string inviteCode)
        {
            return quizRunner.GetQuizResults(inviteCode);
        }

        public void EndQuiz(string inviteCode)
        {
            quizRunner.EndQuiz(inviteCode);
        }
    }
}