using System;
using System.Text;
using Domain.Common;
using Domain.Configs;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.Interface;

namespace Services;

public class QueueConsumerService : IDisposable, IQueueConsumerService
{
    private readonly IQueueConnection _connection;
    private readonly IHubContext<ChatHubService> _context;
    private readonly QueueConfig _queueConfig;
    private IModel _channel;

    public QueueConsumerService(IOptions<WebAppSettings> settings, IHubContext<ChatHubService> context,
        IQueueConnection connection)
    {
        _queueConfig = settings.Value.QueueConfig;
        _context = context;
        _connection = connection;
    }

    public void Dispose()
    {
        _channel?.Close();
        _channel?.Dispose();
    }

    public void StartListening()
    {
        _channel = _connection.GetConnection().CreateModel();
        _channel.BasicQos(0, 10, false);
        var queue = _queueConfig.QueueName;
        _channel.QueueDeclare(queue,
            true,
            false,
            false,
            null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += MessageReceived;
        _channel.BasicConsume(queue,
            true,
            consumer);
    }

    public async void MessageReceived(object model, BasicDeliverEventArgs eventArgs)
    {
        var body = eventArgs.Body.ToArray();
        var data = JsonConvert.DeserializeObject<MessageViewModel>(Encoding.UTF8.GetString(body));
        try
        {
            await _context.Clients.All.SendAsync(ChatConstant.ReceiveChatMessage, data);
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to read the rabbitMq message.", ex);
        }
    }
}