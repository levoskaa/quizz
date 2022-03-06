using FluentValidation;
using Quizz.Common.ErrorHandling;
using Quizz.GameService.Application.Commands;

namespace Quizz.GameService.Application.Validators
{
    public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
    {
        public UpdateGameCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty()
                .WithMessage(ValidationError.GameNameRequired);

            RuleFor(command => command.GameId).NotEmpty()
                .WithMessage(ValidationError.GameIdRequired);

            RuleFor(command => command.UserId).NotEmpty()
                .WithMessage(ValidationError.UserIdRequired);
        }
    }
}