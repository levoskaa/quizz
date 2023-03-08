﻿using Autofac;
using FluentValidation;
using MediatR;
using Quizz.Common.DataAccess;
using Quizz.Common.Services;
using Quizz.Questions.Application.Behaviors;
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

            // TODO
            // Validators
            //builder.RegisterAssemblyTypes(typeof(CreateQuestionsCommandValidator).Assembly)
            //    .Where(type => type.IsClosedTypeOf(typeof(IValidator<>)))
            //    .AsImplementedInterfaces()
            //    .InstancePerDependency();

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
}