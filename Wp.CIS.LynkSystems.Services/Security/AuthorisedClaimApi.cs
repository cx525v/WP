using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using Wp.CIS.LynkSystems.Model.Authentication;
using Wp.CIS.LynkSystems.Interfaces.Secuirity;

namespace Wp.CIS.LynkSystems.Services.Security
{
    public class AuthorisedClaimApi  : IAuthorisedClaimApi
    {
        
        

        public AuthorisedClaimApi()
        {   

        }

        public string IssuerServer { get; set; }
        public string AudianceServer { get; set; }
        public string ExpirationTime { get; set; }
        public string SymmetricSecurityKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string CreateToken(CISUser model)
        {
            try
            {

                if (model.UserRole == null)
                {
                    model.UserRole = GetUserRole(model);
                }
                //For now including the Admin Role if the Role of user is not assigned.
                List<Claim> userClaims = new List<Claim>(){
                                                new Claim(model.UserRole.Role.ToString() ?? "CIS_STAGE_Reader", "true")
                                                 };
                var claims = new[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, model.UserName)
                    }.Union(userClaims);

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SymmetricSecurityKey));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: IssuerServer,
                    audience: AudianceServer,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(ExpirationTime)),
                    signingCredentials: signingCredentials
                    );

                
                var token = new 
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    expiration = jwtSecurityToken.ValidTo
                };
                return  token.ToString();
            }
            catch(Exception ex)
            {
                //logging of the error
                return ex.ToString();
            }     
            
        }

        public UserRole GetUserRole(CISUser user)
        {
            UserRole _userRole = new UserRole();

            //Logic to call AD for user Role goes here.
            //_userRole.Role = Roles.Admin;
            return _userRole;
        }

    }
}
