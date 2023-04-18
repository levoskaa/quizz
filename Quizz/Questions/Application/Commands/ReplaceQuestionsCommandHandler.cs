using AutoMapper;
using MediatR;
using Quizz.Common.Dtos;
using Quizz.Common.Models;
using Quizz.Questions.Data.Repositories;
using Quizz.Questions.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quizz.Questions.Application.Commands;

public class ReplaceQuestionsCommandHandler
    : IRequestHandler<ReplaceQuestionsCommand, IEnumerable<string>>
{
    private readonly IQuestionRepository questionRepository;
    private readonly IMapper mapper;

    public ReplaceQuestionsCommandHandler(
        IQuestionRepository questionRepository,
        IMapper mapper)
    {
        this.questionRepository = questionRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<string>> Handle(ReplaceQuestionsCommand request, CancellationToken cancellationToken)
    {
        // Remove old questions
        await RemoveQuestions(request.QuestionIds);
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

    private async Task RemoveQuestions(IEnumerable<string> questionIds)
    {
        var questionsToRemove = await questionRepository
            .FilterAsync(question => questionIds.Contains(question.Id.ToString()));
        foreach (var question in questionsToRemove)
        {
            questionRepository.Remove(question);
        }
    }

    private Question CreateNewQuestion(QuestionDto questionDto)
    {
        Question question = null;
        switch (questionDto.Type)
        {
            case QuestionType.FindOrder:
                question = new FindOrderQuestion(
                    questionDto.Text,
                    questionDto.Index,
                    questionDto.TimeLimitInSeconds);
                question.ReplaceAnswerPossibilities(
                    mapper.Map<IEnumerable<Common.Models.FindOrderAnswer>>(questionDto.AnswerPossibilities));
                break;

            case QuestionType.MultipleChoice:
                question = new MultipleChoiceQuestion(
                    questionDto.Text,
                    questionDto.Index,
                    questionDto.TimeLimitInSeconds);
                question.ReplaceAnswerPossibilities(
                    mapper.Map<IEnumerable<Common.Models.MultipleChoiceAnswer>>(questionDto.AnswerPossibilities));
                break;

            case QuestionType.TrueOrFalse:
                question = new TrueOrFalseQuestion(
                    questionDto.Text,
                    questionDto.Index,
                    questionDto.TimeLimitInSeconds,
                    (bool)questionDto.CorrectAnswer);
                question.ReplaceAnswerPossibilities(
                    mapper.Map<IEnumerable<Common.Models.Answer>>(questionDto.AnswerPossibilities));
                break;

            case QuestionType.FreeText:
                question = new FreeTextQuestion(
                    questionDto.Text,
                    questionDto.Index,
                    questionDto.TimeLimitInSeconds);
                question.ReplaceAnswerPossibilities(
                    mapper.Map<IEnumerable<Common.Models.Answer>>(questionDto.AnswerPossibilities));
                break;
        }
        if (question == null)
        {
            throw new QuestionsDomainException("Question could not be created");
        }
        return question;
    }
}