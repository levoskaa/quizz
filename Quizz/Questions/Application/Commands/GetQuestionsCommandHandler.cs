using MediatR;
using Quizz.Common.Models;
using Quizz.Questions.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quizz.Questions.Application.Commands;

public class GetQuestionsCommandHandler : IRequestHandler<GetQuestionsCommand, IEnumerable<Question>>
{
    private readonly IQuestionRepository questionRepository;

    public GetQuestionsCommandHandler(IQuestionRepository questionRepository)
    {
        this.questionRepository = questionRepository;
    }

    public async Task<IEnumerable<Question>> Handle(GetQuestionsCommand request, CancellationToken cancellationToken)
    {
        var questions = await questionRepository
            .FilterAsync((question) => request.QuestionIds.Contains(question.Id));
        return questions;
    }
}