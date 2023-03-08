using MediatR;
using Quizz.Common.Dtos;
using System.Collections.Generic;

namespace Quizz.Questions.Application.Commands;

public class ReplaceQuestionsCommand : IRequest<IEnumerable<string>>
{
    public IEnumerable<string> QuestionIds { get; set; }

    public IEnumerable<QuestionDto> QuestionDtos { get; set; }
}