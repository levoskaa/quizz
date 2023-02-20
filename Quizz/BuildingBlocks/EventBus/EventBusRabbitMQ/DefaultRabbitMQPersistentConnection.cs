using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.IO;
using System.Net.Sockets;

namespace Quizz.BuildingBlocks.EventBusRabbitMQ;

public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection
{
    private readonly IConnectionFactory connectionFactory;
    private readonly ILogger<DefaultRabbitMQPersistentConnection> logger;
    private readonly int retryCount;
    private IConnection connection;
    private bool disposed;

    private object sync_root = new();

    public DefaultRabbitMQPersistentConnection(
        IConnectionFactory connectionFactory,
        ILogger<DefaultRabbitMQPersistentConnection> logger,
        int retryCount = 5)
    {
        this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.retryCount = retryCount;
    }

    public bool IsConnected => connection != null && connection.IsOpen && !disposed;

    public IModel CreateModel()
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
        }

        return connection.CreateModel();
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;

        try
        {
            connection.ConnectionShutdown -= OnConnectionShutdown;
            connection.CallbackException -= OnCallbackException;
            connection.ConnectionBlocked -= OnConnectionBlocked;
            connection.Dispose();
        }
        catch (IOException e)
        {
            logger.LogCritical(e.ToString());
        }
    }

    public bool TryConnect()
    {
        logger.LogInformation("RabbitMQ Client is trying to connect");

        lock (sync_root)
        {
            var policy = RetryPolicy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    logger.LogWarning(ex, "RabbitMQ Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                }
            );

            policy.Execute(() =>
            {
                connection = connectionFactory.CreateConnection();
            });

            if (IsConnected)
            {
                connection.ConnectionShutdown += OnConnectionShutdown;
                connection.CallbackException += OnCallbackException;
                connection.ConnectionBlocked += OnConnectionBlocked;

                logger.LogInformation("RabbitMQ Client acquired a persistent connection to '{HostName}' and is subscribed to failure events", connection.Endpoint.HostName);

                return true;
            }
            else
            {
                logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened");

                return false;
            }
        }
    }

    private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
    {
        if (disposed)
        {
            return;
        }

        logger.LogWarning("A RabbitMQ connection has shut down. Trying to reconnect...");

        TryConnect();
    }

    private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
    {
        if (disposed)
        {
            return;
        }

        logger.LogWarning("A RabbitMQ connection threw an exception. Trying to reconnect...");

        TryConnect();
    }

    private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
    {
        if (disposed)
        {
            return;
        }

        logger.LogWarning("A RabbitMQ connection has shut down. Trying to reconnect...");

        TryConnect();
    }
}