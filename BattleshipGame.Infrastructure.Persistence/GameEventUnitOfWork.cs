using frm.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BattleshipGame.Infrastructure.Persistence;

public class GameEventUnitOfWork(DbContext context) : IQueryableUnitOfWork
{
    private IDbContextTransaction? _transaction;

    private async Task DisposeTransactionAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.DisposeAsync();
        }

        _transaction = null;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
        {
            return;
        }

        _transaction = await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);

        if (_transaction is not null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await DisposeTransactionAsync();
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await DisposeTransactionAsync();
        }
    }

    public void RollbackChanges()
    {
        context.ChangeTracker.Entries()
            .ToList()
            .ForEach(entry => entry.State = EntityState.Unchanged);

        _transaction?.Rollback();
        _transaction?.Dispose();
        _transaction = null;
    }

    #region Controle the state of the entities

    public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
    {
        return context.Set<TEntity>();
    }

    public void Attach<TEntity>(TEntity item) where TEntity : class
    {
        context.Entry<TEntity>(item).State = EntityState.Unchanged;
    }

    public void SetModified<TEntity>(TEntity item) where TEntity : class
    {
        context.Entry<TEntity>(item).State = EntityState.Modified;
    }

    public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
    {
        context.Entry<TEntity>(original).CurrentValues.SetValues(current);
    }

    #endregion

    #region Raw Sql queries and commands

    public async Task<IEnumerable<TEntity>> ExecuteQueryAsync<TEntity>(string sqlQuery, params object[] parameters)
    {
        if (string.IsNullOrWhiteSpace(sqlQuery))
        {
            throw new ArgumentNullException(nameof(sqlQuery));
        }

        return await context.Database.SqlQueryRaw<TEntity>(sqlQuery, parameters).ToListAsync();
    }

    public async Task<int> ExecuteCommandAsync(string sqlCommand, params object[] parameters)
    {
        if (string.IsNullOrWhiteSpace(sqlCommand))
        {
            throw new ArgumentNullException(nameof(sqlCommand));
        }

        return await context.Database.ExecuteSqlRawAsync(sqlCommand, parameters);
    }

    #endregion

    public void Dispose()
    {
        _transaction?.Dispose();
        context.Dispose();
    }
}