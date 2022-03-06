using FluentValidation;
using Quizz.Common.ErrorHandling;
using Quizz.GameService.Application.Commands;

namespace Quizz.GameService.Application.Validators
{
    public class DeleteGameCommandValidator : AbstractValidator<DeleteGameCommand>
    {
        public DeleteGameCommandValidator()
        {
            RuleFor(command => command.GameId).NotEmpty()
                .WithMessage(ValidationError.GameIdRequired);

            RuleFor(command => command.UserId).NotEmpty()
                .WithMessage(ValidationError.UserIdRequired);
        }
    }
}