using Quizz.GameService.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Linq;
using QuestionType = Quizz.Questions.Protos.QuestionType;

namespace Quizz.GameService.Infrastructure.Services;

public class QuestionsGrpcConverter
{
    public static IEnumerable<Common.Models.Answer> AnswerProtosToAnswers(QuestionType questionType,
        IEnumerable<Questions.Protos.Answer> answers)
    {
        return questionType switch
        {
            QuestionType.MultipleChoice => MultipleChoiceAnswerProtosToAnswers(answers),
            QuestionType.FindOrder => FindOrderAnswerProtosToAnswers(answers),
            QuestionType.FreeText => FreeTextAnswerProtosToAnswers(answers),
            _ => Enumerable.Empty<Common.Models.Answer>(),
        };
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
                CorrectAnswer = questionDto.CorrectAnswer ?? false,
            };
            mappedQuestionDto.AnswerPossibilites.AddRange(AnswerDtosToAnswerDtoProtos(questionDto.AnswerPossibilities));
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
                DisplayIndex = answerDto.DisplayIndex ?? 0,
                CorrectIndex = answerDto.CorrectIndex ?? 0,
                IsCorrect = answerDto.IsCorrect ?? false,
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
            case QuestionType.FindOrder:
                mappedQuestion = new Common.Models.FindOrderQuestion(
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
                    question.TimeLimitInSeconds,
                    question.CorrectAnswer);
                break;

            case QuestionType.FreeText:
                mappedQuestion = new Common.Models.FreeTextQuestion(
                    question.Text,
                    question.Index,
                    question.TimeLimitInSeconds);
                break;
        }
        if (question == null)
        {
            throw new GameServiceDomainException("Question could not be mapped");
        }
        mappedQuestion.ReplaceAnswerPossibilities(AnswerProtosToAnswers(question.Type, question.AnswerPossibilites));
        return mappedQuestion;
    }

    private static IEnumerable<Common.Models.MultipleChoiceAnswer> MultipleChoiceAnswerProtosToAnswers(IEnumerable<Questions.Protos.Answer> answers)
    {
        var mappedAnswers = new List<Common.Models.MultipleChoiceAnswer>();
        foreach (var answerDto in answers)
        {
            var mappedAnswer = new Common.Models.MultipleChoiceAnswer
            {
                Id = answerDto.Id,
                Text = answerDto.Text,
                DisplayIndex = answerDto.DisplayIndex,
                IsCorrect = answerDto.IsCorrect,
            };
            mappedAnswers.Add(mappedAnswer);
        }
        return mappedAnswers;
    }

    private static IEnumerable<Common.Models.FindOrderAnswer> FindOrderAnswerProtosToAnswers(IEnumerable<Questions.Protos.Answer> answers)
    {
        var mappedAnswers = new List<Common.Models.FindOrderAnswer>();
        foreach (var answerDto in answers)
        {
            var mappedAnswer = new Common.Models.FindOrderAnswer
            {
                Id = answerDto.Id,
                Text = answerDto.Text,
                DisplayIndex = answerDto.DisplayIndex,
                CorrectIndex = answerDto.CorrectIndex,
            };
            mappedAnswers.Add(mappedAnswer);
        }
        return mappedAnswers;
    }

    private static IEnumerable<Common.Models.Answer> FreeTextAnswerProtosToAnswers(IEnumerable<Questions.Protos.Answer> answers)
    {
        var mappedAnswers = new List<Common.Models.Answer>();
        foreach (var answerDto in answers)
        {
            var mappedAnswer = new Common.Models.Answer
            {
                Id = answerDto.Id,
                Text = answerDto.Text,
            };
            mappedAnswers.Add(mappedAnswer);
        }
        return mappedAnswers;
    }
}