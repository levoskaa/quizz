using FluentValidation;
using Quizz.Common.ErrorHandling;
using Quizz.Questions.Application.Commands;

namespace Quizz.Questions.Application.Validators
{
    public class GetQuestionsCommandValidator : AbstractValidator<GetQuestionsCommand>
    {
        public GetQuestionsCommandValidator()
        {
            RuleFor(command => command.QuestionIds).NotNull()
                .WithMessage(ValidationError.QuestionIdsRequired);
        }
    }
}