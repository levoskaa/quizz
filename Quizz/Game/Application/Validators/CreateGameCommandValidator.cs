using FluentValidation;
using Quizz.Common.ErrorHandling;
using Quizz.GameService.Application.Commands;

namespace Quizz.GameService.Application.Validators;

public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty()
            .WithMessage(ValidationError.GameNameRequired);
    }
}
