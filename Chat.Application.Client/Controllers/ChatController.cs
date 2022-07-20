using Domain.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Chat.Application.Client.Controllers;

//protecting this controller routes is not part of demo 
public class ChatController : Controller
{
    private readonly ClientAppSettings _settings;

    public ChatController(IOptions<ClientAppSettings> settings)
    {
        _settings = settings.Value;
    }

    public IActionResult Chat()
    {
        var user = TempData["LoggedInUser"];
        if (user == null) return RedirectToAction("Login", "Account");
        TempData["ChatAppWebServiceUrl"] = _settings.ChatAppWebServiceUrl;
        return View();
    }
}