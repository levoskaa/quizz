using Autofac;
using FluentValidation;
using Quizz.Common.Services;
using Quizz.QuizRunner.Application.Validators;
using Quizz.QuizRunner.Infrastructure.Services;

namespace Quizz.QuizRunner.Infrastructure;

public class SignalRModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Services
        builder.RegisterType<IdentityService>()
            .As<IIdentityService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<QuizRunnerService>()
            .As<IQuizRunnerService>()
            .InstancePerLifetimeScope();

        // Validators
        builder.RegisterAssemblyTypes(typeof(InitGameCommandValidator).Assembly)
            .Where(type => type.IsClosedTypeOf(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .InstancePerDependency();
    }
}