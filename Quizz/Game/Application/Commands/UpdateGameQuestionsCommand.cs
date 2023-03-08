using MediatR;
using Quizz.Common.Dtos;
using System.Collections.Generic;

namespace Quizz.GameService.Application.Commands;

public class UpdateGameQuestionsCommand : IRequest
{
    public int GameId { get; init; }
    public string UserId { get; init; }
    public IEnumerable<QuestionDto> Questions { get; init; }

    public UpdateGameQuestionsCommand(int gameId,
        string userId,
        IEnumerable<QuestionDto> questions)
    {
        GameId = gameId;
        UserId = userId;
        Questions = questions;
    }
}