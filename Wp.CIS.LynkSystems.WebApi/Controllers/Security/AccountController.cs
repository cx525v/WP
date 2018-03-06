using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using Wp.CIS.LynkSystems.Model.Authentication;
using Microsoft.AspNetCore.Cors;
using Wp.CIS.LynkSystems.Interfaces.Secuirity;
using Microsoft.Extensions.Localization;

namespace Wp.CIS.LynkSystems.WebApi.Controllers.Security
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/Account")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class AccountController : Controller
    {
        private IConfigurationRoot _configurationRoot;        
        private readonly IStringLocalizer<AccountController> _localizer;

        private IAuthorisedClaimApi _claimApi;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationRoot"></param>
        /// <param name="localizer"></param>
        /// <param name="claimApi"></param>
        public AccountController( IConfigurationRoot configurationRoot, IStringLocalizer<AccountController> localizer, IAuthorisedClaimApi claimApi)
        {
            this._localizer = localizer;            
            _configurationRoot = configurationRoot;
            _claimApi = claimApi;
            _claimApi.IssuerServer=_configurationRoot["Token:Issuer"];
            _claimApi.AudianceServer = _configurationRoot["Token:Audience"];
            _claimApi.ExpirationTime = _configurationRoot["Token:ExpirationTime"];
            _claimApi.SymmetricSecurityKey = _configurationRoot["Token:Key"];
        }

        /// <summary>
        /// Authenticated user gets a token to access the other restful api resources.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("token")]
        public IActionResult CreateToken([FromBody] CISUser model)
        {            
            try
            {            
                if(model== null )
                {
                    return BadRequest(this._localizer?["ModelIsNull"]?.Value);
                }

                if (model != null && model.UserName == null)
                {
                    return Unauthorized();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(this._localizer?["InvalidModelState"]?.Value);
                }



                //if (model.DomainName != null && model.DomainName == "LYNK")
                //{

                var authToken = _claimApi.CreateToken(model);
                //var authToken = GetToken(model);


                if (authToken.ToUpper().IndexOf(" ERROR") > 0 || authToken.ToUpper().IndexOf("NULLREFERENCE") > 0)
                {
                    return BadRequest(this._localizer?["ExceptionWhileCreatingAToken"]?.Value);
                }
                else
                {
                    return Ok(JsonConvert.SerializeObject(authToken));
                }  

                    
                //}

                //return Unauthorized();
            }
            catch (Exception )
            {
                
                return StatusCode((int)HttpStatusCode.InternalServerError, "error while creating token");
            }


            
        }
        
        
    }
}