using System;
using Domain.Configs;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Services.Interface;

namespace Services.Helper;

public class QueueConnectionHelper : IQueueConnection
{
    private static IConnection _connection;
    private readonly ConnectionFactory _connectionFactory;

    public QueueConnectionHelper(IOptions<WebAppSettings> settings)
    {
        var queueConfig = settings.Value.QueueConfig;
        if (queueConfig.Port == 0)
            queueConfig.Port = 5672;
        _connectionFactory = new ConnectionFactory
        {
            HostName = queueConfig.HostName,
            Port = queueConfig.Port,
            UserName = queueConfig.UserName,
            Password = queueConfig.Password,
            ContinuationTimeout = TimeSpan.FromSeconds(120),
            HandshakeContinuationTimeout = TimeSpan.FromSeconds(120)
        };
    }

    public IConnection GetConnection()
    {
        return _connection ??= _connectionFactory.CreateConnection();
    }
}