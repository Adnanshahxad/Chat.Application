using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces;

public interface IAccountRepository : IUnitOfWork
{
    Task<IdentityResult> RegisterAsync(RegisterViewModel model, CancellationToken cancellationToken = default);
    Task<ResultModel> LoginAsync(LoginViewModel user, CancellationToken cancellationToken = default);
}