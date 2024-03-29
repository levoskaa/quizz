﻿using RabbitMQ.Client;
using System;

namespace Quizz.BuildingBlocks.EventBusRabbitMQ;

public interface IRabbitMQPersistentConnection : IDisposable
{
    bool IsConnected { get; }

    bool TryConnect();

    IModel CreateModel();
}
