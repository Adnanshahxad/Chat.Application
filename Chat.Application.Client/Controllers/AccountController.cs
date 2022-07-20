using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Chat.Application.Client.Helper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Chat.Application.Client.Controllers;

public class AccountController : Controller
{
    private readonly WebServiceHttpHelper _httpHelper;
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger, WebServiceHttpHelper httpHelper)
    {
        _logger = logger;
        _httpHelper = httpHelper;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        try
        {
            model.RememberMe = true;
            var result = await _httpHelper.LoginAsync(model);
            if (result.IsSucceeded)
            {
                TempData["LoggedInUser"] = result.UserName;
                return RedirectToAction("Chat", "Chat");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to login.", ex);
        }

        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        try
        {
            var result = await _httpHelper.RegisterAsync(model);
            if (result.IsSucceeded)
                return RedirectToAction("Chat", "Chat");
            ViewBag.ErrorMessage = result.Message;
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to register.", ex);
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}