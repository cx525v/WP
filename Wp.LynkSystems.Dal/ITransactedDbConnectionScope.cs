namespace Wp.CIS.LynkSystems.Dal
{
    public interface ITransactedDbConnectionScope : IDbConnectionScope
    {
        void Complete();
    }
}