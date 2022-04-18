using FluentValidation;
using Quizz.GameService.Application.Commands;

namespace Quizz.GameService.Application.Validators;

public class UpdateGameQuestionsCommandValidator : AbstractValidator<UpdateGameQuestionsCommand>
{
    public UpdateGameQuestionsCommandValidator()
    { }
}