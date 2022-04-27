using AutoMapper;
using Grpc.Core;
using MediatR;
using Quizz.Questions.Application.Commands;
using Quizz.Questions.Infrastructure.Services;
using Quizz.Questions.Protos;
using System.Linq;
using System.Threading.Tasks;

namespace Quizz.Questions.gRPC;

public class QuestionsService : Protos.Questions.QuestionsBase
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public QuestionsService(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    public override async Task<GetQuestionsReply> GetQuestions(GetQuestionsRequest request, ServerCallContext context)
    {
        var getQuestionsCommand = new GetQuestionsCommand
        {
            QuestionIds = request.QuestionIds.ToList(),
        };
        var questions = await mediator.Send(getQuestionsCommand);
        var reply = new GetQuestionsReply();
        reply.Questions.AddRange(GrpcConverter.QuestionsToQuestionProtos(questions));
        return reply;
    }

    public override async Task<ReplaceQuestionsReply> ReplaceQuestions(ReplaceQuestionsRequest request, ServerCallContext context)
    {
        var replaceQuestionsCommand = new ReplaceQuestionsCommand
        {
            QuestionIds = request.QuestionIds.ToList(),
            QuestionDtos = GrpcConverter.QuestionDtoProtosToQuestionDtos(request.QuestionDtos.ToList()),
        };
        var newQuestionIds = await mediator.Send(replaceQuestionsCommand);
        var reply = new ReplaceQuestionsReply();
        reply.NewQuestionIds.AddRange(newQuestionIds);
        return reply;
    }
}