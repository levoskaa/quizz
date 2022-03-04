using Autofac;
using MediatR;
using Quizz.Common.Services;
using Quizz.GameService.Application.Behaviors;
using Quizz.GameService.Infrastructure.Repositories;

namespace Quizz.GameService.Infrastructure
{
    public class GameServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IGameRepository).Assembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<IdentityService>()
                .As<IIdentityService>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ValidatorBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerLifetimeScope();
        }
    }
}