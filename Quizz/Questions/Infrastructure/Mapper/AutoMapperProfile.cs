using AutoMapper;
using Quizz.Common.Dtos;
using Quizz.Common.Models;

namespace Questions.Infrastructure.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Quizz.Common.Models.Question, Quizz.Questions.Protos.Question>()
                .ReverseMap();

            CreateMap<Quizz.Common.Dtos.AnswerDto, Quizz.Common.Models.Answer>()
                .Include<AnswerDto, MultipleChoiceAnswer>()
                .Include<AnswerDto, FindOrderAnswer>();

            CreateMap<AnswerDto, MultipleChoiceAnswer>();
            CreateMap<AnswerDto, FindOrderAnswer>();
        }
    }
}