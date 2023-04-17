using Quizz.GameService.Protos;
using Quizz.QuizRunner.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quizz.QuizRunner.Infrastructure.Services
{
    public class GamesGrpcConverter
    {
        public static IEnumerable<Common.Models.Answer> AnswerProtosToAnswers(GameService.Protos.QuestionType questionType,
        IEnumerable<GameService.Protos.Answer> answers)
        {
            return questionType switch
            {
                GameService.Protos.QuestionType.MultipleChoice => MultipleChoiceAnswerProtosToAnswers(answers),
                QuestionType.FindOrder => FindOrderAnswerProtosToAnswers(answers),
                QuestionType.FreeText => FreeTextAnswerProtosToAnswers(answers),
                _ => Enumerable.Empty<Common.Models.Answer>(),
            };
        }

        public static IEnumerable<Common.Models.Question> QuestionProtosToQuestions(IEnumerable<GameService.Protos.Question> questions)
        {
            var mappedQuestions = new List<Common.Models.Question>();
            foreach (var question in questions)
            {
                mappedQuestions.Add(CreateNewQuestion(question));
            }
            return mappedQuestions;
        }

        private static Common.Models.Question CreateNewQuestion(GameService.Protos.Question question)
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
                throw new QuizRunnerDomainException("Question could not be mapped");
            }
            mappedQuestion.Id = Guid.Parse(question.Id);
            mappedQuestion.ReplaceAnswerPossibilities(AnswerProtosToAnswers(question.Type, question.AnswerPossibilities));
            return mappedQuestion;
        }

        private static IEnumerable<Common.Models.MultipleChoiceAnswer> MultipleChoiceAnswerProtosToAnswers(IEnumerable<GameService.Protos.Answer> answers)
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

        private static IEnumerable<Common.Models.FindOrderAnswer> FindOrderAnswerProtosToAnswers(IEnumerable<GameService.Protos.Answer> answers)
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

        private static IEnumerable<Common.Models.Answer> FreeTextAnswerProtosToAnswers(IEnumerable<Answer> answers)
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
}