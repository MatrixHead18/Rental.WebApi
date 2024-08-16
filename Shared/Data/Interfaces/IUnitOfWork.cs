namespace Rental.WebApi.Shared.Data.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> PersistChangesAsync(CancellationToken cancellationToken = default);
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
