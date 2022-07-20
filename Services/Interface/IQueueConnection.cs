using RabbitMQ.Client;

namespace Services.Interface;

public interface IQueueConnection
{
    IConnection GetConnection();
}