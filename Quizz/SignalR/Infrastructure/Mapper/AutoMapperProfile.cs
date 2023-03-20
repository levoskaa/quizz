using AutoMapper;
using Quizz.Common.Models;
using Quizz.Common.ViewModels;
using Quizz.SignalR.Application.Commands;
using Quizz.SignalR.Application.Dtos;

namespace Quizz.SignalR.Infrastructure.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<InitGameDto, InitGameCommand>();

            // Questions
            CreateMap<Question, QuestionViewModel>()
                .IncludeAllDerived();
            CreateMap<FindOrderQuestion, QuestionViewModel>();
            CreateMap<MultipleChoiceQuestion, QuestionViewModel>();
            CreateMap<TrueOrFalseQuestion, QuestionViewModel>();
            CreateMap<FreeTextQuestion, QuestionViewModel>();

            // Answers
            CreateMap<Answer, AnswerViewModel>()
                .IncludeAllDerived();
            CreateMap<MultipleChoiceAnswer, AnswerViewModel>();
            CreateMap<FindOrderAnswer, AnswerViewModel>();
        }
    }
}