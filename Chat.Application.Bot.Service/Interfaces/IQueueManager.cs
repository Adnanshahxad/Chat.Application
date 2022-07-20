namespace Chat.Application.Bot.Service.Interfaces;

public interface IQueueManager
{
    void SendMessage(object dynamicObject);
}