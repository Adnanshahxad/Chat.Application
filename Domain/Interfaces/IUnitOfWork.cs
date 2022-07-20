﻿using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}