using Autofac;
using Quizz.Common.Services;
using Quizz.SignalR.Infrastructure.Services;

namespace Quizz.GameService.Infrastructure;

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
    }
}