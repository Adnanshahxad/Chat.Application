using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public sealed class ChatRepository : IChatRepository
{
    private readonly ChatDbContext _databaseContext;

    public ChatRepository(ChatDbContext dbContext)
    {
        _databaseContext = dbContext;
    }

    public async Task<bool> SaveMessageAsync(MessageViewModel input)
    {
        var chat = new Chat
        {
            Message = input.Message,
            DateTime = DateTime.Now,
            UserName = input.UserName
        };
        _databaseContext.Chats.Add(chat);
        await _databaseContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<MessageViewModel>> LoadChats(int messageSize = 50,
        string chatRoom = ChatConstant.DefaultChatRoom)
    {
        var chats = await _databaseContext.Chats.Where(x => x.ChatRoom == chatRoom).OrderByDescending(x => x.DateTime)
            .Take(messageSize).OrderBy(x => x.DateTime).Select(x => new MessageViewModel
            {
                UserName = x.UserName,
                Message = x.Message,
                DateTime = x.DateTime.ToString("h:m, M/d/yyyy")
            }).ToListAsync();
        return chats;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _databaseContext.SaveChangesAsync(cancellationToken);
        return 0;
    }
}