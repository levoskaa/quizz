using AutoMapper;
using Quizz.SignalR.Application.Commands;
using Quizz.SignalR.Application.Dtos;

namespace Quizz.SignalR.Infrastructure.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<InitGameDto, InitGameCommand>();
        }
    }
}