using CommonSolution.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CommonSolution.CrossCutting.PostgresSQL
{
    public sealed class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly ApplicationDbContext _db;
        private IDbContextTransaction? _tx;

        public UnitOfWork(ApplicationDbContext db) => _db = db;

        public async Task BeginTransaction(CancellationToken ct = default)
        {
            if (_tx is not null) return;
            _tx = await _db.Database.BeginTransactionAsync(ct);
        }

        public async Task Commit(CancellationToken ct = default)
        {
            if (_tx is null) return;
            await _db.SaveChangesAsync(ct);
            await _tx.CommitAsync(ct);
            await _tx.DisposeAsync();
            _tx = null;
        }

        public async Task Rollback(CancellationToken ct = default)
        {
            if (_tx is null) return;
            await _tx.RollbackAsync(ct);
            await _tx.DisposeAsync();
            _tx = null;
        }

        public async Task ExecuteResilient(Func<CancellationToken, Task> action, CancellationToken ct = default)
        {
            var strategy = _db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var localTx = await _db.Database.BeginTransactionAsync(ct);
                try
                {
                    await action(ct);
                    await _db.SaveChangesAsync(ct);
                    await localTx.CommitAsync(ct);
                }
                catch
                {
                    await localTx.RollbackAsync(ct);
                    throw;
                }
            });
        }

        public async ValueTask DisposeAsync()
        {
            if (_tx is not null)
            {
                await _tx.DisposeAsync();
                _tx = null;
            }
        }
    }
}
