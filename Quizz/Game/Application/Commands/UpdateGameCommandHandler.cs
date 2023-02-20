using AutoMapper;
using MediatR;
using Quizz.Common.ErrorHandling;
using Quizz.GameService.Application.Models;
using Quizz.GameService.Data.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quizz.GameService.Application.Commands;

public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand>
{
    private readonly IGameRepository gameRepository;
    private readonly IMapper mapper;

    public UpdateGameCommandHandler(IGameRepository gameRepository, IMapper mapper)
    {
        this.gameRepository = gameRepository;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateGameCommand updateGameCommand, CancellationToken cancellationToken)
    {
        var game = await gameRepository.GetAsync(updateGameCommand.GameId);
        ValidateCommand(game, updateGameCommand);
        DoUpdate(game, updateGameCommand);
        await gameRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return Unit.Value;
    }

    private void ValidateCommand(Game game, UpdateGameCommand updateGameCommand)
    {
        if (game.OwnerId != updateGameCommand.UserId)
        {
            throw new ForbiddenException();
        }
    }

    private void DoUpdate(Game game, UpdateGameCommand updateGameCommand)
    {
        game.Name = updateGameCommand.Name;
        game.UpdatedAt = DateTime.Now;
    }
}