using System;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;
using Services.Interface;

namespace Services;

public sealed class ChatHubService : Hub, IChatHubService
{
    private readonly IChatRepository _chatRepository;
    private readonly IHubContext<ChatHubService> _context;
    private readonly IBotServiceHttpHelper _httpHelper;

    public ChatHubService(IChatRepository chatRepository, IHubContext<ChatHubService> context,
        IBotServiceHttpHelper httpHelper)
    {
        _chatRepository = chatRepository;
        _context = context;
        _httpHelper = httpHelper;
    }

    public async Task<bool> SendMessageAsync(MessageViewModel input)
    {
        var symbol = ExtractSymbolFromMessage(input.Message);
        if (symbol != null)
            try
            {
                return await _httpHelper.StockCodeAsync(symbol);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to send message to bot service.", ex);
            }

        await _context.Clients.All.SendAsync(ChatConstant.ReceiveChatMessage, input);
        return await _chatRepository.SaveMessageAsync(input);
    }

    public override async Task<Task> OnConnectedAsync()
    {
        var chats = await _chatRepository.LoadChats();
        await _context.Clients.Client(Context.ConnectionId).SendAsync(ChatConstant.ReceiveChatHistory, chats);
        return base.OnConnectedAsync();
    }

    internal static string ExtractSymbolFromMessage(string message)
    {
        if (message.StartsWith("/stock="))
        {
            var parsedMessage = message.TrimEnd().Split('=');
            var symbol = parsedMessage.Length > 0 ? parsedMessage[1] : null;
            return symbol;
        }

        return null;
    }
}