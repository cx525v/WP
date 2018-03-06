using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wp.CIS.LynkSystems.Model.Error;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/Error")]
    public class ErrorController : Controller
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        // POST: api/Error
        [HttpPost]
        public string Post([FromBody]ErrorModel value)
        {
            return string.Empty;
        }
        
    }
}
