using System.Threading;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace DAL.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ChatDbContext _databaseContext;

    public UnitOfWork(ChatDbContext dbContext)
    {
        _databaseContext = dbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _databaseContext.SaveChangesAsync(cancellationToken);
    }
}