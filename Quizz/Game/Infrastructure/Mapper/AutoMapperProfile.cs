using AutoMapper;
using Quizz.Common.ViewModels;
using Quizz.GameService.Application.Commands;
using Quizz.GameService.Application.Models;
using Quizz.GameService.Infrastructure.Exceptions;

namespace Quizz.GameService.Infrastructure.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateGameCommand, Game>();

            CreateMap<GameServiceDomainException, ErrorViewModel>()
                .ForMember(x => x.Errors, options => options.MapFrom(x => x.ErrorCodes));
        }
    }
}