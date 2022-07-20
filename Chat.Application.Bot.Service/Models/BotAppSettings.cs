namespace Chat.Application.Bot.Service.Models;

//general application settings 
public class BotAppSettings
{
    public QueueConfig QueueConfig { get; set; }
    public string CsvFilePath { get; set; }
}