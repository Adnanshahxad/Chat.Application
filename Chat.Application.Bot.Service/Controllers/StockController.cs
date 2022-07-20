using System;
using System.Net;
using System.Net.Http;
using Chat.Application.Bot.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Application.Bot.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class StockController : ControllerBase
{
    private readonly IStockManager _stockManager;

    public StockController(IStockManager stockManager)
    {
        _stockManager = stockManager;
    }

    [HttpPost]
    [Route("{stockSymbol}")]
    public HttpResponseMessage StockCode(string stockSymbol)
    {
        if (string.IsNullOrWhiteSpace(stockSymbol))
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Please provide a valid stock symbol."),
                ReasonPhrase = "Invalid stock symbol"
            };
        }
        try
        {
            _stockManager.StockCode(stockSymbol);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
        catch (Exception e)
        {
            throw new Exception($"Unable to process stock symbol({stockSymbol}) due to internal server error.", e);
        }
    }
}