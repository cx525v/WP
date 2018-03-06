namespace Wp.CIS.LynkSystems.Dal
{
    /// <summary>
    /// Gets an existing IDbConnection if one exists, or creates a new one. 
    /// This allows for nested routines to use the same IDbConnection which eliminates unnecessary promotion to the DTC.
    /// </summary>
    public interface IDbConnectionScopeFactory
    {
        /// <summary>
        /// Gets an unopened database connection and returns the wrapper
        /// </summary>
        /// <returns></returns>
        IDbConnectionScope GetConnectionScope();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ITransactedDbConnectionScope GetTransactedConnectionScope();
    }
}