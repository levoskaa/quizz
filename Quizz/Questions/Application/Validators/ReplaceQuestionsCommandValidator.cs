using FluentValidation;
using Quizz.Common.ErrorHandling;
using Quizz.Questions.Application.Commands;

namespace Quizz.Questions.Application.Validators
{
    public class ReplaceQuestionsCommandValidator : AbstractValidator<ReplaceQuestionsCommand>
    {
        public ReplaceQuestionsCommandValidator()
        {
            RuleFor(command => command.QuestionIds).NotNull()
                .WithMessage(ValidationError.QuestionIdsRequired);

            RuleFor(command => command.QuestionDtos).NotNull()
                .WithMessage(ValidationError.NewQuestionsRequired);
        }
    }
}