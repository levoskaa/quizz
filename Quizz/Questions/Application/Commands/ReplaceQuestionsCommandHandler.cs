﻿using MediatR;
using Quizz.Common.Dtos;
using Quizz.Common.Models;
using Quizz.Questions.Data.Repositories;
using Quizz.Questions.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quizz.Questions.Application.Commands;

public class ReplaceQuestionsCommandHandler
    : IRequestHandler<ReplaceQuestionsCommand, IEnumerable<string>>
{
    private readonly IQuestionRepository questionRepository;

    public ReplaceQuestionsCommandHandler(IQuestionRepository questionRepository)
    {
        this.questionRepository = questionRepository;
    }

    public async Task<IEnumerable<string>> Handle(ReplaceQuestionsCommand request, CancellationToken cancellationToken)
    {
        var questionsToRemove = await questionRepository
            .FilterAsync(question => request.QuestionIds.Contains(question.Id.ToString()));
        foreach (var question in questionsToRemove)
        {
            questionRepository.Remove(question);
        }
        var newQuestionIds = new List<string>();
        foreach (var dto in request.QuestionDtos)
        {
            var newQuestion = CreateNewQuestion(dto);
            questionRepository.Add(newQuestion);
            newQuestionIds.Add(newQuestion.Id.ToString());
        }
        await questionRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return newQuestionIds;
    }

    private Question CreateNewQuestion(QuestionDto questionDto)
    {
        Question question = null;
        switch (questionDto.Type)
        {
            case QuestionType.FindCorrectOrder:
                question = new FindCorrectOrderQuestion(
                    questionDto.Text,
                    questionDto.Index,
                    questionDto.TimeLimitInSeconds);
                break;

            case QuestionType.MultipleChoice:
                question = new MultipleChoiceQuestion(
                    questionDto.Text,
                    questionDto.Index,
                    questionDto.TimeLimitInSeconds);
                break;

            case QuestionType.TrueOrFalse:
                question = new TrueOrFalseQuestion(
                    questionDto.Text,
                    questionDto.Index,
                    questionDto.TimeLimitInSeconds);
                break;

            case QuestionType.TypeInAnswer:
                question = new TypeInAnswerQuestion(
                    questionDto.Text,
                    questionDto.Index,
                    questionDto.TimeLimitInSeconds);
                break;
        }
        if (question == null)
        {
            throw new QuestionsDomainException("Question could not be created");
        }
        return question;
    }
}