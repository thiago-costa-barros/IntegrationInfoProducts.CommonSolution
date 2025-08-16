using System.Data;

namespace CommonSolution.Database.Interfaces
{
    public interface IUnitOfWork
    {
        Task BeginTransaction(CancellationToken ct = default);
        Task Commit(CancellationToken ct = default);
        Task Rollback(CancellationToken ct = default);
        Task ExecuteResilient(Func<CancellationToken, Task> action, CancellationToken ct = default);
    }
}
