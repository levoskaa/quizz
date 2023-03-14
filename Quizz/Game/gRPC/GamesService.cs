using Grpc.Core;
using MediatR;
using Quizz.Common.ErroHandling;
using Quizz.GameService.Application.Commands;
using Quizz.GameService.Infrastructure.Services;
using Quizz.GameService.Protos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quizz.GameService.gRPC
{
    public class GamesService : Games.GamesBase
    {
        private readonly IMediator mediator;

        public GamesService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<GetGameQuestionsReply> GetGameQuestions(GetGameQuestionsRequest request, ServerCallContext context)
        {
            var getGameQuestionsCommand = new GetGameQuestionsCommand(request.GameId, request.UserId);
            IEnumerable<Common.Models.Question> questions;
            try
            {
                questions = await mediator.Send(getGameQuestionsCommand);
                
            } catch (EntityNotFoundException)
            {
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Game not found"));
            }
            var reply = new GetGameQuestionsReply();
            reply.Questions.AddRange(GamesGrpcConverter.QuestionsToQuestionProtos(questions));
            return reply;
        }
    }
}
