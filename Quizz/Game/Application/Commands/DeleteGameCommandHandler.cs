using MediatR;
using Quizz.BuildingBlocks.EventBus.Abstractions;
using Quizz.Common.ErrorHandling;
using Quizz.GameService.Application.IntegrationEvents;
using Quizz.GameService.Application.Models;
using Quizz.GameService.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Quizz.GameService.Application.Commands;

public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand>
{
    private readonly IGameRepository gameRepository;
    private readonly IEventBus eventBus;

    public DeleteGameCommandHandler(IGameRepository gameRepository, IEventBus eventBus)
    {
        this.gameRepository = gameRepository;
        this.eventBus = eventBus;
    }

    public async Task<Unit> Handle(DeleteGameCommand deleteGameCommand, CancellationToken cancellationToken)
    {
        var game = await gameRepository.GetAsync(deleteGameCommand.GameId);
        ValidateCommand(deleteGameCommand, game);
        gameRepository.Remove(game);
        await gameRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        FireIntegrationEvent(game.QuestionIds);
        return Unit.Value;
    }

    private void ValidateCommand(DeleteGameCommand command, Game game)
    {
        if (command.UserId != game.OwnerId)
        {
            throw new ForbiddenException();
        }
    }

    private void FireIntegrationEvent(IEnumerable<Guid> questionIds)
    {
        // Integration Event to delete related questions
        var @event = new GameDeletedIntegrationEvent(questionIds);
        eventBus.Publish(@event);
    }
}