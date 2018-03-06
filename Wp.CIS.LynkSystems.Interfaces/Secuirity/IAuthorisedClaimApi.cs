using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Model.Authentication;
namespace Wp.CIS.LynkSystems.Interfaces.Secuirity
{
    public interface IAuthorisedClaimApi
    {
        string IssuerServer { get; set; }

        string AudianceServer { get; set; }
        string ExpirationTime { get; set; }
        string SymmetricSecurityKey { get; set; }
        
        string CreateToken(CISUser user);

    }
}