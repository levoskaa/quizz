﻿using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quizz.BuildingBlocks.EventBus;
using Quizz.BuildingBlocks.EventBus.Abstractions;
using Quizz.BuildingBlocks.EventBusRabbitMQ;
using Quizz.BuildingBlocks.IntegrationEventLog.Services;
using Quizz.GameService.Application.IntegrationEvents;
using Quizz.GameService.Data;
using RabbitMQ.Client;

namespace Quizz.Questions.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    // TODO: move to Autofac module
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventBus>(sp =>
        {
            var subscriptionClientName = configuration["SubscriptionClientName"];
            var retryCount = int.Parse(configuration["EventBusRetryCount"]);
            var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
            var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
            var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

            return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
        });

        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

            var factory = new ConnectionFactory()
            {
                HostName = configuration["EventBusHost"],
                DispatchConsumersAsync = true
            };

            var retryCount = int.Parse(configuration["EventBusRetryCount"]);

            return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
        });

        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

        return services;
    }

    public static IServiceCollection AddIntegrations(this IServiceCollection services)
    {
        services.AddTransient<IIntegrationEventLogService>(sp =>
        {
            var dbContext = sp.GetRequiredService<GameContext>();
            return new IntegrationEventLogService(dbContext.Database.GetDbConnection());
        });

        services.AddTransient<IGameServiceIntegrationEventService, GameServiceIntegrationEventService>();

        return services;
    }
}