using Autofac;
using FluentValidation;
using MediatR;
using Quizz.BuildingBlocks.EventBus.Abstractions;
using Quizz.Common.DataAccess;
using Quizz.Common.Services;
using Quizz.Questions.Application.Behaviors;
using Quizz.Questions.Application.IntegrationEvents;
using Quizz.Questions.Application.Validators;
using Quizz.Questions.Data;
using Quizz.Questions.Data.Repositories;

namespace Quizz.Questions.Infrastructure
{
    public class QuestionsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Repositories
            builder.RegisterAssemblyTypes(typeof(QuestionRepository).Assembly)
                .Where(type => type.IsClosedTypeOf(typeof(IRepository<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Services
            builder.RegisterType<IdentityService>()
                .As<IIdentityService>()
                .InstancePerLifetimeScope();

            // Validators
            builder.RegisterAssemblyTypes(typeof(GetQuestionsCommandValidator).Assembly)
                .Where(type => type.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            // Behaviors
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerDependency();

            // Integration event handlers
            builder.RegisterAssemblyTypes(typeof(GameDeletedIntegrationEventHandler).Assembly)
                .Where(type => type.IsClosedTypeOf(typeof(IIntegrationEventHandler<>)))
                .AsSelf()
                .InstancePerDependency();

            // Dapper
            builder.RegisterType<DapperContext>()
                .AsSelf()
                .SingleInstance();
        }
    }
}