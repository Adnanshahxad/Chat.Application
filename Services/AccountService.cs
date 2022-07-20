using System.Threading;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Services.Interface;

namespace Services;

public sealed class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ResultModel> LoginAsync(LoginViewModel model, CancellationToken cancellationToken = default)
    {
        return await _accountRepository.LoginAsync(model, cancellationToken);
    }

    public async Task<ResultModel> RegisterAsync(RegisterViewModel model, CancellationToken cancellationToken = default)
    {
        var result = await _accountRepository.RegisterAsync(model, cancellationToken);
        if (result.Succeeded)
            await _accountRepository.SaveChangesAsync(cancellationToken);

        return new ResultModel(result.ToString());
    }
}