using RabbitMQ.Client.Events;

namespace Services.Interface;

public interface IQueueConsumerService
{
    void StartListening();
    void MessageReceived(object model, BasicDeliverEventArgs eventArgs);
}