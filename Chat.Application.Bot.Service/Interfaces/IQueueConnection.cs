using RabbitMQ.Client;

namespace Chat.Application.Bot.Service.Interfaces;

public interface IQueueConnection
{
    IConnection GetConnection();
}