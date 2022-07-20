using System;
using Chat.Application.Bot.Service.Helper;
using Chat.Application.Bot.Service.Interfaces;
using Chat.Application.Bot.Service.Models;
using Microsoft.Extensions.Options;

namespace Chat.Application.Bot.Service.Manager;

public class StockManager : IStockManager
{
    private const string MessageOwner = "Bot";
    private readonly IQueueManager _queueManager;
    private readonly BotAppSettings _settings;

    public StockManager(IQueueManager queueManager, IOptions<BotAppSettings> settings)
    {
        _queueManager = queueManager;
        _settings = settings.Value;
    }

    public void StockCode(string stockSymbol)
    {
        var stockModel = new StockDto(MessageOwner);
        if (string.IsNullOrWhiteSpace(stockSymbol))
        {
            stockModel.Message = "Invalid stock symbol.";
            _queueManager.SendMessage(stockModel);
            return;
        }

        //reading stock price from csv file
        try
        {
            var price = StockFileHelper.GetStockPrice(_settings.CsvFilePath, stockSymbol);
            if (price != null)
                stockModel.Message = $"{stockSymbol} quote is {price} per share.";
            else
                stockModel.Message = $"Invalid stock symbol({stockSymbol}) or data does not exist for {stockSymbol}.";

            //sending message to rabbitMQ , so the consumer can get the stock price
            _queueManager.SendMessage(stockModel);
        }
        catch (Exception e)
        {
            stockModel.Message = $"Unable to get the quote for symbol({stockSymbol}).";
            _queueManager.SendMessage(stockModel);
            throw new Exception(stockModel.Message, e);
        }
    }
}