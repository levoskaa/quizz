using Autofac;
using FluentValidation;
using MediatR;
using Quizz.Common.DataAccess;
using Quizz.Common.Services;
using Quizz.GameService.Application.Behaviors;
using Quizz.GameService.Application.Validators;
using Quizz.GameService.Data;
using Quizz.GameService.Data.Repositories;

namespace Quizz.GameService.Infrastructure;

public class GameServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Repositories
        builder.RegisterAssemblyTypes(typeof(GameRepository).Assembly)
            .Where(type => type.IsClosedTypeOf(typeof(IRepository<>)))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        // Services
        builder.RegisterType<IdentityService>()
            .As<IIdentityService>()
            .InstancePerLifetimeScope();

        // Validators
        builder.RegisterAssemblyTypes(typeof(CreateGameCommandValidator).Assembly)
            .Where(type => type.IsClosedTypeOf(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .InstancePerDependency();

        // Behaviors
        builder.RegisterGeneric(typeof(ValidatorBehavior<,>))
            .As(typeof(IPipelineBehavior<,>))
            .InstancePerDependency();

        // Dapper
        builder.RegisterType<DapperContext>()
            .AsSelf()
            .SingleInstance();
    }
}