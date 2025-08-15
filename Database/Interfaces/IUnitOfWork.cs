using System.Data;

namespace CommonSolution.Database.Interfaces
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync(IsolationLevel isolation = IsolationLevel.ReadCommitted, CancellationToken ct = default);
        Task CommitAsync(CancellationToken ct = default);
        Task RollbackAsync(CancellationToken ct = default);
        Task ExecuteResilientAsync(Func<CancellationToken, Task> action, CancellationToken ct = default);
    }
}
