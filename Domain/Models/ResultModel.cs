namespace Domain.Models;

public class ResultModel
{
    public ResultModel(string message, string userName = null)
    {
        Message = message;
        IsSucceeded = string.IsNullOrWhiteSpace(message) || message == "Succeeded";
        UserName = userName;
    }

    public string Message { get; set; }
    public string UserName { get; set; }
    public bool IsSucceeded { get; set; }
}