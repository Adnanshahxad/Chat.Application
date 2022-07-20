using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Models;

namespace Domain.Interfaces;

public interface IChatRepository : IUnitOfWork
{
    Task<bool> SaveMessageAsync(MessageViewModel input);
    Task<List<MessageViewModel>> LoadChats(int messageSize = 50, string chatRoom = ChatConstant.DefaultChatRoom);
}