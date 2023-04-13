using Autofac;
using FluentValidation;
using Quizz.Common.Services;
using Quizz.SignalR.Application.Validators;
using Quizz.SignalR.Infrastructure.Services;

namespace Quizz.SignalR.Infrastructure;

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