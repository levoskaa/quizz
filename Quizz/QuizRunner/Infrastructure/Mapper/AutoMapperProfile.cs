using AutoMapper;
using Quizz.Common.Models;
using Quizz.Common.ViewModels;
using Quizz.QuizRunner.Application.Commands;
using Quizz.QuizRunner.Application.Dtos;
using Quizz.QuizRunner.Application.Models;
using Quizz.QuizRunner.Application.ViewModels;
using System.Collections.Generic;

namespace Quizz.QuizRunner.Infrastructure.Mapper
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

            // Results
            CreateMap<IEnumerable<ParticipantResult>, QuizResultsViewModel>()
                .ForMember(dest => dest.ParticipantResults, options => options.MapFrom(src => src));
            CreateMap<ParticipantResult, ParticipantResultViewModel>();
        }
    }
}