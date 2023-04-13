using FluentValidation;
using Quizz.Common.ErrorHandling;
using Quizz.SignalR.Application.Commands;

namespace Quizz.SignalR.Application.Validators
{
    public class InitGameCommandValidator : AbstractValidator<InitGameCommand>
    {
        public InitGameCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty()
                .WithMessage(ValidationError.UserIdRequired);

            RuleFor(command => command.GameId).NotEmpty()
                .WithMessage(ValidationError.GameIdRequired);
        }
    }
}