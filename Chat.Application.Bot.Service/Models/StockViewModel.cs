using Chat.Application.Bot.Service.Helper;

namespace Chat.Application.Bot.Service.Models;

public class StockDto
{
    public StockDto(string userName)
    {
        UserName = userName;
        DateTime = DateHelper.CurrentDate();
    }

    public string UserName { get; set; }
    public string Message { get; set; }
    public string DateTime { get; set; }
}