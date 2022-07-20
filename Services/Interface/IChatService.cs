using System.Threading.Tasks;
using Domain.Models;

namespace Services.Interface;

public interface IChatHubService
{
    Task<bool> SendMessageAsync(MessageViewModel input);
}