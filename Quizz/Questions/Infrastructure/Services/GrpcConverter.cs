using Quizz.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quizz.Questions.Infrastructure.Services;

public class GrpcConverter
{
    public static IEnumerable<Common.Dtos.QuestionDto> QuestionDtoProtosToQuestionDtos(IEnumerable<Questions.Protos.QuestionDto> questionDtos)
    {
        var mappedQuestionDtos = new List<Common.Dtos.QuestionDto>();
        foreach (var questionDto in questionDtos)
        {
            var questionType = (Common.Models.QuestionType)Enum.Parse(typeof(Common.Models.QuestionType), questionDto.Type.ToString());
            var mappedQuestionDto = new Common.Dtos.QuestionDto
            {
                Text = questionDto.Text,
                Type = questionType,
                Index = questionDto.Index,
                TimeLimitInSeconds = questionDto.TimeLimitInSeconds,
                AnswerPossibilities = AnswerDtoProtosToAnswerDtos(questionDto.AnswerPossibilites),
                CorrectAnswer = questionDto.CorrectAnswer,
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
                DisplayIndex = answerDto.DisplayIndex,
                CorrectIndex = answerDto.CorrectIndex,
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
            mappedQuestion.AnswerPossibilites.AddRange(AnswersToAnswerProtos(question.Type, question.AnswerPossibilities));
            if (question.Type == QuestionType.TrueOrFalse)
            {
                mappedQuestion.CorrectAnswer = (question as TrueOrFalseQuestion).CorrectAnswer;
            }
            mappedQuestionDtos.Add(mappedQuestion);
        }
        return mappedQuestionDtos;
    }

    public static IEnumerable<Protos.Answer> AnswersToAnswerProtos(QuestionType questionType,
        IEnumerable<Answer> answers)
    {
        return questionType switch
        {
            QuestionType.MultipleChoice => MultipleChoiceAnswersToAnswerProtos(answers.Cast<MultipleChoiceAnswer>()),
            QuestionType.FindOrder => FindOrderAnswersToAnswerProtos(answers.Cast<FindOrderAnswer>()),
            QuestionType.FreeText => FreeTextAnswersToAnswerProtos(answers),
            _ => Enumerable.Empty<Protos.Answer>(),
        };
    }

    private static IEnumerable<Protos.Answer> MultipleChoiceAnswersToAnswerProtos(IEnumerable<MultipleChoiceAnswer> answers)
    {
        var mappedAnswerDtos = new List<Protos.Answer>();
        foreach (var answer in answers)
        {
            var mappedAnswerDto = new Questions.Protos.Answer
            {
                Id = answer.Id,
                Text = answer.Text,
                IsCorrect = answer.IsCorrect,
                DisplayIndex = answer.DisplayIndex,
            };
            mappedAnswerDtos.Add(mappedAnswerDto);
        }
        return mappedAnswerDtos;
    }

    private static IEnumerable<Protos.Answer> FindOrderAnswersToAnswerProtos(IEnumerable<FindOrderAnswer> answers)
    {
        var mappedAnswerDtos = new List<Protos.Answer>();
        foreach (var answer in answers)
        {
            var mappedAnswerDto = new Questions.Protos.Answer
            {
                Id = answer.Id,
                Text = answer.Text,
                DisplayIndex = answer.DisplayIndex,
                CorrectIndex = answer.CorrectIndex,
            };
            mappedAnswerDtos.Add(mappedAnswerDto);
        }
        return mappedAnswerDtos;
    }

    private static IEnumerable<Protos.Answer> FreeTextAnswersToAnswerProtos(IEnumerable<Answer> answers)
    {
        var mappedAnswerDtos = new List<Protos.Answer>();
        foreach (var answer in answers)
        {
            var mappedAnswerDto = new Questions.Protos.Answer
            {
                Id = answer.Id,
                Text = answer.Text,
            };
            mappedAnswerDtos.Add(mappedAnswerDto);
        }
        return mappedAnswerDtos;
    }
}