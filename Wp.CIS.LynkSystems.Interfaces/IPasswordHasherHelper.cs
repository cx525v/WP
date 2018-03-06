namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface IPasswordHasherHelper
    {
        bool ComparePasswords(string plainTextPassword, byte[] dbPassword, byte[] dbSalt);
    }
}