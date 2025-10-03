using Microsoft.EntityFrameworkCore;

namespace frm.Infrastructure.Persistence;

/// <summary>
/// The UnitOfWork contract for EF implementation
/// <remarks>
/// This contract extends IUnitOfWork for use with EF code
/// </remarks>
/// </summary>
public interface IQueryableUnitOfWork : IUnitOfWork, ISql
{
    /// <summary>
    /// Returns an IDbSet instance for access to entities of the given type in the context, the ObjectStateManager, and the underlying store. 
    /// </summary>
    /// <returns></returns>
    DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

    /// <summary>
    /// To associate existing entities in aggregates.
    /// When you want to avoid loading related entities just to set up relationships.
    /// This avoids an unnecessary query like: var table = await _context.Tables.FindAsync(id);
    /// </summary>
    /// <param name="item">Attach this item into "ObjectStateManager"</param>
    void Attach<TEntity>(TEntity item) where TEntity : class;

    /// <summary>
    /// To track detached entities in updates.
    /// Suppose you're using DDD or mapping DTOs to domain models in the app layer, and you get a detached entity back.
    /// Or Unchanged if you're not modifying anything
    /// </summary>
    /// <param name="item">The entity item to set as modified</param>
    void SetModified<TEntity>(TEntity item) where TEntity : class;

    /// <summary>
    /// Apply current values in <paramref name="original"/>
    /// </summary>
    /// <typeparam name="TEntity">The type of entity</typeparam>
    /// <param name="original">The original entity</param>
    /// <param name="current">The current entity</param>
    void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class;
}