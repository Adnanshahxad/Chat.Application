using System;
using System.Text;
using Chat.Application.Bot.Service.Interfaces;
using Chat.Application.Bot.Service.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Chat.Application.Bot.Service.Manager;

public class QueueManager : IQueueManager
{
    private readonly IQueueConnection _connection;
    private readonly QueueConfig _queueConfig;

    public QueueManager(IOptions<BotAppSettings> settings, IQueueConnection connection)
    {
        _queueConfig = settings.Value.QueueConfig;
        _connection = connection;
    }

    public void SendMessage(object dynamicObject)
    {
        var connection = _connection.GetConnection();
        try
        {
            using var channel = connection.CreateModel();
            var queue = _queueConfig.QueueName;
            channel.QueueDeclare(queue, true, false, false, null);
            var message = JsonConvert.SerializeObject(dynamicObject);
            var body = Encoding.UTF8.GetBytes(message);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            //publish message to the queue 
            channel.BasicPublish("", queue, properties, body);
        }
        catch (Exception ex)
        {
            //we should handle these exception gracefully
            throw new Exception("Unable to publish the message to exchange", ex);
        }
    }
}