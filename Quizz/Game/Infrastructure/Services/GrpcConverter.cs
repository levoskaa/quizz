using Quizz.GameService.Infrastructure.Exceptions;
using Quizz.Questions.Protos;
using System.Collections.Generic;

namespace Quizz.GameService.Infrastructure.Services;

public class GrpcConverter
{
    public static IEnumerable<Common.Models.Answer> AnswerProtosToAnswers(IEnumerable<Questions.Protos.Answer> answers)
    {
        var mappedAnswers = new List<Common.Models.Answer>();
        foreach (var answerDto in answers)
        {
            var mappedAnswer = new Common.Models.Answer
            {
                Text = answerDto.Text,
                Index = answerDto.Index,
                IsCorrect = answerDto.IsCorrect,
            };
            mappedAnswers.Add(mappedAnswer);
        }
        return mappedAnswers;
    }

    public static IEnumerable<Common.Models.Question> QuestionProtosToQuestions(IEnumerable<Questions.Protos.Question> questions)
    {
        var mappedQuestions = new List<Common.Models.Question>();
        foreach (var question in questions)
        {
            mappedQuestions.Add(CreateNewQuestion(question));
        }
        return mappedQuestions;
    }

    public static IEnumerable<Questions.Protos.QuestionDto> QuestionDtosToQuestionDtoProtos(IEnumerable<Common.Dtos.QuestionDto> questionDtos)
    {
        var mappedQuestionDtos = new List<Questions.Protos.QuestionDto>();
        foreach (var questionDto in questionDtos)
        {
            var mappedQuestionDto = new Questions.Protos.QuestionDto
            {
                Text = questionDto.Text,
                Type = (Questions.Protos.QuestionType)questionDto.Type,
                Index = questionDto.Index,
                TimeLimitInSeconds = questionDto.TimeLimitInSeconds,
            };
            mappedQuestionDto.AnswerPossibilites.AddRange(AnswerDtosToAnswerDtoProtos(questionDto.AnswerPossibilites));
            mappedQuestionDtos.Add(mappedQuestionDto);
        }
        return mappedQuestionDtos;
    }

    public static IEnumerable<Questions.Protos.AnswerDto> AnswerDtosToAnswerDtoProtos(IEnumerable<Common.Dtos.AnswerDto> answerDtos)
    {
        var mappedAnswerDtos = new List<Questions.Protos.AnswerDto>();
        foreach (var answerDto in answerDtos)
        {
            var mappedAnswerDto = new Questions.Protos.AnswerDto
            {
                Text = answerDto.Text,
                Index = answerDto.Index,
                IsCorrect = answerDto.IsCorrect,
            };
            mappedAnswerDtos.Add(mappedAnswerDto);
        }
        return mappedAnswerDtos;
    }

    private static Common.Models.Question CreateNewQuestion(Questions.Protos.Question question)
    {
        Common.Models.Question mappedQuestion = null;
        switch (question.Type)
        {
            case QuestionType.FindCorrectOrder:
                mappedQuestion = new Common.Models.FindCorrectOrderQuestion(
                    question.Text,
                    question.Index,
                    question.TimeLimitInSeconds);
                break;

            case QuestionType.MultipleChoice:
                mappedQuestion = new Common.Models.MultipleChoiceQuestion(
                    question.Text,
                    question.Index,
                    question.TimeLimitInSeconds);
                break;

            case QuestionType.TrueOrFalse:
                mappedQuestion = new Common.Models.TrueOrFalseQuestion(
                    question.Text,
                    question.Index,
                    question.TimeLimitInSeconds);
                break;

            case QuestionType.TypeInAnswer:
                mappedQuestion = new Common.Models.TypeInAnswerQuestion(
                    question.Text,
                    question.Index,
                    question.TimeLimitInSeconds);
                break;
        }
        if (question == null)
        {
            throw new GameServiceDomainException("Question could not be mapped");
        }
        mappedQuestion.ReplaceAnswerPossibilities(AnswerProtosToAnswers(question.AnswerPossibilites));
        return mappedQuestion;
    }
}