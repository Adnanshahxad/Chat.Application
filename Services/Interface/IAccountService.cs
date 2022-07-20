using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

namespace Services.Interface;

public interface IAccountService
{
    Task<ResultModel> LoginAsync(LoginViewModel model, CancellationToken cancellationToken);
    Task<ResultModel> RegisterAsync(RegisterViewModel model, CancellationToken cancellationToken = default);
}