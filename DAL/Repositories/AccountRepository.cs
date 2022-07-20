using System.Threading;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories;

public sealed class AccountRepository : IAccountRepository
{
    private readonly ChatDbContext _databaseContext;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountRepository(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager, ChatDbContext dbContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _databaseContext = dbContext;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterViewModel model,
        CancellationToken cancellationToken = default)
    {
        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded) await _signInManager.SignInAsync(user, false);

        return result;
    }

    public async Task<ResultModel> LoginAsync(LoginViewModel user, CancellationToken cancellationToken = default)
    {
        var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
        if (result.Succeeded)
        {
            var dbUser = await _signInManager.UserManager.FindByEmailAsync(user.Email);
            return new ResultModel(result.ToString(), dbUser.UserName);
        }

        return new ResultModel(result.ToString());
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _databaseContext.SaveChangesAsync(cancellationToken);
        return 0;
    }
}