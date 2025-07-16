namespace frm.Infrastructure.Persistence;

/// <summary>
/// Contract for "UnitOfWork Pattern". For more related info see http://martinfowler.com/eaaCatalog/unitOfWork.html
/// In this solution, the Unit Of Work is implemented using the out-of-box 
/// Entity Framework Context (EF DbContext) persistence engine.
/// But in order to comply with the PI (Persistence Ignorant) principle in our Domain, we implement this interface/contract. 
/// This interface/contract should be complied by any UoW implementation to be used with this Domain.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Commit all changes made in a container.
    /// </summary>
    ///<remarks>
    /// If the entity has fixed properties and any optimistic concurrency problem exists, then an exception is thrown
    ///</remarks>
    Task<int> CommitAsync(CancellationToken cancellationToken = default);

    // TODO: If the entity has fixed properties and any optimistic concurrency problem exists, then the local changes should be discarded - Client wins

    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rollback tracked changes. See references of UnitOfWork pattern
    /// </summary>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}