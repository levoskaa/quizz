using Quizz.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Quizz.GameService.Infrastructure.Services
{
    public class GamesGrpcConverter
    {
        public static IEnumerable<Protos.Question> QuestionsToQuestionProtos(IEnumerable<Question> questions)
        {
            var mappedQuestionDtos = new List<Protos.Question>();
            foreach (var question in questions)
            {
                var mappedQuestion = new Protos.Question
                {
                    Text = question.Text,
                    Type = (Protos.QuestionType)question.Type,
                    Index = question.Index,
                    TimeLimitInSeconds = question.TimeLimitInSeconds,
                };
                mappedQuestion.AnswerPossibilities.AddRange(AnswersToAnswerProtos(question.Type, question.AnswerPossibilities));
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
                var mappedAnswerDto = new Protos.Answer
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
                var mappedAnswerDto = new Protos.Answer
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
                var mappedAnswerDto = new Protos.Answer
                {
                    Id = answer.Id,
                    Text = answer.Text,
                };
                mappedAnswerDtos.Add(mappedAnswerDto);
            }
            return mappedAnswerDtos;
        }
    }
}
