using AutoMapper;

namespace Questions.Infrastructure.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // TODO: implement mapping for descendant properties
            CreateMap<Quizz.Common.Models.Question, Quizz.Questions.Protos.Question>()
                .ReverseMap();
        }
    }
}