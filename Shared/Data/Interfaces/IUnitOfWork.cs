using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Rental.WebApi.Shared.Data.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> PersistChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default);
        IExecutionStrategy CreateExecutionStrategy();
    }
}
