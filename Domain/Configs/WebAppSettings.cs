namespace Domain.Configs;

//general application settings 
public class WebAppSettings
{
    public QueueConfig QueueConfig { get; set; }
    public string ChatBotServiceUrl { get; set; }
}