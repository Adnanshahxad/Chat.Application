using System;

namespace Chat.Application.Bot.Service.Helper;

public class DateHelper
{
    public static string CurrentDate()
    {
        var d = DateTime.Now;
        return d.ToString("h:m, M/d/yyyy");
    }
}