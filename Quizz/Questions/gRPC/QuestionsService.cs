﻿using AutoMapper;
using Grpc.Core;
using MediatR;
using Quizz.Questions.Application.Commands;
using Quizz.Questions.Protos;
using System;
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

    public override async Task<ReplaceQuestionsReply> ReplaceQuestions(ReplaceQuestionsRequest request, ServerCallContext context)
    {
        var replaceQuestionsCommand = new ReplaceQuestionsCommand
        {
            QuestionIds = request.QuestionIds.ToList(),
            QuestionDtos = MapProtosToQuestionDtos(request.QuestionDtos.ToList()),
        };
        var newQuestionIds = await mediator.Send(replaceQuestionsCommand);
        var reply = new ReplaceQuestionsReply();
        reply.NewQuestionIds.AddRange(newQuestionIds);
        return reply;
    }

    private IEnumerable<Common.Dtos.QuestionDto> MapProtosToQuestionDtos(IEnumerable<Protos.QuestionDto> questionDtos)
    {
        var mappedQuestionDtos = new List<Common.Dtos.QuestionDto>();
        foreach (var questionDto in questionDtos)
        {
            var mappedQuestionDto = new Common.Dtos.QuestionDto
            {
                Text = questionDto.Text,
                Type = (Common.Models.QuestionType)Enum.Parse(typeof(Common.Models.QuestionType), questionDto.Type.ToString()),
                Index = questionDto.Index,
                TimeLimitInSeconds = questionDto.TimeLimitInSeconds,
                AnswerPossibilites = MapProtosToAnswerDtos(questionDto.AnswerPossibilites)
            };
            mappedQuestionDtos.Add(mappedQuestionDto);
        }
        return mappedQuestionDtos;
    }

    private IEnumerable<Common.Dtos.AnswerDto> MapProtosToAnswerDtos(IEnumerable<Questions.Protos.AnswerDto> answerDtos)
    {
        var mappedAnswerDtos = new List<Common.Dtos.AnswerDto>();
        foreach (var answerDto in answerDtos)
        {
            var mappedAnswerDto = new Common.Dtos.AnswerDto
            {
                Text = answerDto.Text,
                Index = answerDto.Index,
                IsCorrect = answerDto.IsCorrect,
            };
            mappedAnswerDtos.Add(mappedAnswerDto);
        }
        return mappedAnswerDtos;
    }
}