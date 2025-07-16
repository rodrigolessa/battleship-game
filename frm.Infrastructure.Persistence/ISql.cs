namespace frm.Infrastructure.Persistence;

/// <summary>
/// Base contract for support 'dialect-specific queries'.
/// </summary>
public interface ISql
{
    /// <summary>
    /// Execute a specific query with underlying persistence store
    /// </summary>
    /// <typeparam name="TEntity">Entity type to map query results</typeparam>
    /// <param name="sqlQuery">Dialect Select Query to Retrieve Data</param>
    /// <param name="parameters">A vector of parameters values</param>
    /// <returns>Enumerable results</returns>
    Task<IEnumerable<TEntity>> ExecuteQueryAsync<TEntity>(string sqlQuery, params object[] parameters);

    ///  <summary>
    ///  Execute arbitrary command into underlying persistence store
    ///  </summary>
    ///  <param name="sqlCommand">Command to execute and made modifications</param>
    ///  <param name="parameters">A vector of parameters values</param>
    ///  <returns>The number of affected records</returns>
    Task<int> ExecuteCommandAsync(string sqlCommand, params object[] parameters);
}