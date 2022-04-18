using Quizz.Common.Dtos;
using System.Collections.Generic;

namespace Quizz.GameService.Application.Dtos;

public class UpdateGameQuestionsDto
{
    public IEnumerable<QuestionDto> Questions { get; init; }
}