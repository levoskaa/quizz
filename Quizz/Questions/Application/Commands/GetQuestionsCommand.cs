using MediatR;
using Quizz.Common.Models;
using System.Collections.Generic;

namespace Quizz.Questions.Application.Commands;

public class GetQuestionsCommand : IRequest<IEnumerable<Question>>
{
    public IEnumerable<string> QuestionIds { get; init; }
}