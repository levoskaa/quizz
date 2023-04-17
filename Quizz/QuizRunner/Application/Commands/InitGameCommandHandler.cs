using AutoMapper;
using MediatR;
using Quizz.Common.ViewModels;
using Quizz.GameService.Protos;
using Quizz.QuizRunner.Application.ViewModels;
using Quizz.QuizRunner.Infrastructure.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static Quizz.GameService.Protos.Games;

namespace Quizz.QuizRunner.Application.Commands
{
    public class InitGameCommandHandler : IRequestHandler<InitGameCommand, GameInitializedViewModel>
    {
        private readonly IQuizRunnerService quizRunnerService;
        private readonly GamesClient gamesClient;
        private readonly IMapper mapper;

        public InitGameCommandHandler(
            IQuizRunnerService quizRunnerService,
            GamesClient gamesClient,
            IMapper mapper)
        {
            this.quizRunnerService = quizRunnerService;
            this.gamesClient = gamesClient;
            this.mapper = mapper;
        }

        public async Task<GameInitializedViewModel> Handle(InitGameCommand command, CancellationToken cancellationToken)
        {
            var grpcRequest = new GetGameQuestionsRequest
            {
                GameId = command.GameId,
                UserId = command.UserId,
            };
            var grpcReply = await gamesClient.GetGameQuestionsAsync(grpcRequest, cancellationToken: cancellationToken);
            var questions = GamesGrpcConverter.QuestionProtosToQuestions(grpcReply.Questions);
            var inviteCode = quizRunnerService.InitQuiz(command.GameId, questions);
            return new GameInitializedViewModel
            {
                InviteCode = inviteCode,
                Questions = mapper.Map<IEnumerable<QuestionViewModel>>(questions),
            };
        }
    }
}