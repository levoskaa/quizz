using System;
using System.Collections.Generic;

namespace Quizz.Questions.Infrastructure.Services;

public class GrpcConverter
{
    public static IEnumerable<Common.Dtos.QuestionDto> QuestionDtoProtosToQuestionDtos(IEnumerable<Questions.Protos.QuestionDto> questionDtos)
    {
        var mappedQuestionDtos = new List<Common.Dtos.QuestionDto>();
        foreach (var questionDto in questionDtos)
        {
            var mappedQuestionDto = new Common.Dtos.QuestionDto
            {
                Text = questionDto.Text,
                Type = (Common.Models.QuestionType)Enum.Parse(typeof(Common.Models.QuestionType), questionDto.Type.ToString()),
                Index = questionDto.Index,
                TimeLimitInSeconds = questionDto.TimeLimitInSeconds,
                AnswerPossibilites = AnswerDtoProtosToAnswerDtos(questionDto.AnswerPossibilites)
            };
            mappedQuestionDtos.Add(mappedQuestionDto);
        }
        return mappedQuestionDtos;
    }

    public static IEnumerable<Common.Dtos.AnswerDto> AnswerDtoProtosToAnswerDtos(IEnumerable<Questions.Protos.AnswerDto> answerDtos)
    {
        var mappedAnswerDtos = new List<Common.Dtos.AnswerDto>();
        foreach (var answerDto in answerDtos)
        {
            var mappedAnswerDto = new Common.Dtos.AnswerDto
            {
                Text = answerDto.Text,
                Index = answerDto.Index,
                IsCorrect = answerDto.IsCorrect,
            };
            mappedAnswerDtos.Add(mappedAnswerDto);
        }
        return mappedAnswerDtos;
    }

    public static IEnumerable<Questions.Protos.Question> QuestionsToQuestionProtos(IEnumerable<Common.Models.Question> questions)
    {
        var mappedQuestionDtos = new List<Questions.Protos.Question>();
        foreach (var question in questions)
        {
            var mappedQuestion = new Questions.Protos.Question
            {
                Text = question.Text,
                Type = (Questions.Protos.QuestionType)question.Type,
                Index = question.Index,
                TimeLimitInSeconds = question.TimeLimitInSeconds,
            };
            mappedQuestion.AnswerPossibilites.AddRange(AnswersToAnswerProtos(question.AnswerPossibilities));
            mappedQuestionDtos.Add(mappedQuestion);
        }
        return mappedQuestionDtos;
    }

    public static IEnumerable<Questions.Protos.Answer> AnswersToAnswerProtos(IEnumerable<Common.Models.Answer> answers)
    {
        var mappedAnswerDtos = new List<Questions.Protos.Answer>();
        foreach (var answer in answers)
        {
            var mappedAnswerDto = new Questions.Protos.Answer
            {
                Text = answer.Text,
                Index = answer.Index,
                IsCorrect = answer.IsCorrect,
            };
            mappedAnswerDtos.Add(mappedAnswerDto);
        }
        return mappedAnswerDtos;
    }
}