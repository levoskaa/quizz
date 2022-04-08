using AutoMapper;
using Quizz.Common.ErroHandling;
using Quizz.Common.ErrorHandling;
using Quizz.Common.ViewModels;
using Quizz.GameService.Application.Commands;
using Quizz.GameService.Application.Dtos;
using Quizz.GameService.Application.Models;
using Quizz.GameService.Application.ViewModels;
using Quizz.GameService.Infrastructure.Exceptions;
using System;

namespace Quizz.GameService.Infrastructure.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Create Game
        CreateMap<CreateGameDto, CreateGameCommand>();

        CreateMap<CreateGameCommand, Game>()
            .ForMember(game => game.OwnerId, options => options.MapFrom(command => command.UserId))
            .ForMember(game => game.UpdatedAt, options => options.MapFrom(_ => DateTime.Now));

        // Get Games
        CreateMap<Game, GameViewModel>();

        // Update Game
        CreateMap<UpdateGameDto, UpdateGameCommand>();

        CreateMap<UpdateGameCommand, Game>()
            .ForMember(game => game.Id, options => options.MapFrom(command => command.GameId))
            .ForMember(game => game.OwnerId, options => options.MapFrom(command => command.UserId))
            .ForMember(game => game.UpdatedAt, options => options.MapFrom(_ => DateTime.Now));

        // Error
        CreateMap<GameServiceDomainException, ErrorViewModel>()
            .ForMember(x => x.Errors, options => options.MapFrom(x => x.ErrorCodes));

        CreateMap<EntityNotFoundException, ErrorViewModel>()
            .ForMember(x => x.Errors, options => options.MapFrom(x => x.ErrorCodes));

        CreateMap<ForbiddenException, ErrorViewModel>();

        CreateMap<Exception, ErrorViewModel>();
    }
}
