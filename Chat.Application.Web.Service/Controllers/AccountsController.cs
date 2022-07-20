using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace Chat.Application.Web.Service.Controllers;

[ApiController]
[Route("api/Accounts")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [Route("register")]
    [HttpPost]
    public async Task<ResultModel> RegisterUser([FromBody] RegisterViewModel model, CancellationToken cancellationToken)
    {
        try
        {
            return await _accountService.RegisterAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to register user.", ex);
        }
    }


    [Route("login")]
    [HttpPost]
    public async Task<ResultModel> Login([FromBody] LoginViewModel model, CancellationToken cancellationToken)
    {
        try
        {
            return await _accountService.LoginAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to login.", ex);
        }

    }

}