namespace Wp.CIS.LynkSystems.Dal
{
    /// <summary>
    /// Gets an IDbExecutor bound to a SQL connection. If a SQL connection exists it is reused, otherwise a new one is created.
    /// </summary>
    public interface IDbExecutorFactory
    {

        /// <summary>
        /// Gets an IDbExecutor without explicitly initializing a new Transaction Scope
        /// </summary>
        /// <returns></returns>
        IDbExecutor GetExecutor();

        /// <summary>
        /// Gets an IDbExecutor scoped within a transaction
        /// </summary>
        /// <returns></returns>
        ITransactedDbExecutor GetTransactedExecutor();
    }
}