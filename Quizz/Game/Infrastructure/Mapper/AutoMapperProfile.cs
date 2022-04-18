﻿using AutoMapper;
using Quizz.Common.ErroHandling;
using Quizz.Common.ErrorHandling;
using Quizz.Common.Models;
using Quizz.Common.ViewModels;
using Quizz.GameService.Application.Commands;
using Quizz.GameService.Application.Dtos;
using Quizz.GameService.Application.Models;
using Quizz.GameService.Application.ViewModels;
using Quizz.GameService.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;

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

        // Get Questions
        CreateMap<Questions.Protos.Question, QuestionViewModel>();
        CreateMap<Common.Models.Question, Questions.Protos.Question>()
            .IncludeAllDerived()
            .ReverseMap();

        CreateMap<FindCorrectOrderQuestion, Questions.Protos.Question>();
        CreateMap<MultipleChoiceQuestion, Questions.Protos.Question>();
        CreateMap<TrueOrFalseQuestion, Questions.Protos.Question>();
        CreateMap<TypeInAnswerQuestion, Questions.Protos.Question>();

        // TODO: probably not needed
        //CreateMap<Common.Models.Question, QuestionViewModel>()
        //    .IncludeAllDerived();
        //CreateMap<FindCorrectOrderQuestion, QuestionViewModel>();
        //CreateMap<MultipleChoiceQuestion, QuestionViewModel>();
        //CreateMap<TrueOrFalseQuestion, QuestionViewModel>();
        //CreateMap<TypeInAnswerQuestion, QuestionViewModel>();

        CreateMap<IEnumerable<Common.Models.Question>, QuestionsListViewModel>()
            .ForMember(vm => vm.Data, options => options.MapFrom(questions => questions));

        // Update Questions
        CreateMap<Questions.Protos.QuestionDto, Common.Dtos.QuestionDto>()
            .ReverseMap();

        // Get Answer
        CreateMap<Answer, AnswerViewModel>();
    }
}