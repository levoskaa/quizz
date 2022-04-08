using FluentValidation;
using MediatR;
using Quizz.GameService.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quizz.GameService.Application.Behaviors;

public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var failures = validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
        {
            var errorCodes = failures.Select(failure => failure.ErrorMessage).ToArray();
            throw new GameServiceDomainException(
                $"Command validation errors for type {typeof(TRequest).Name}",
                errorCodes);
        }
        var response = await next();

        return response;
    }
}
