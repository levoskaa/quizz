using AutoMapper;
using Grpc.Core;
using MediatR;
using Quizz.Questions.Application.Commands;
using Quizz.Questions.Protos;
using System.Collections.Generic;
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
        reply.Questions.AddRange(mapper.Map<IEnumerable<Question>>(questions));
        return reply;
    }
}