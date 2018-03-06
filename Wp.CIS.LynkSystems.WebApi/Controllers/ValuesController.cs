using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Options;
using Wp.CIS.LynkSystems.Services;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Contracts.Models;
using System.Threading;
using Worldpay.Logging.Providers.Log4Net.Facade;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        #region Private Fields

        private IOptions<Settings> _optionsAccessor;
        private readonly ILoggingFacade _loggingFacade;
        #endregion

        #region Public Constructors

        /// <summary>
        /// 
        /// </summary>
        public ValuesController(IOptions<Settings> optionsAccessor, ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting ValuesController", "ValuesController.cs", "ValuesController"), CancellationToken.None);

            this._optionsAccessor = optionsAccessor;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET api/values
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(string))]
        public IEnumerable<string> Get()
        {
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting get Value ", "ValueController.cs", "Get"), CancellationToken.None);
            if (!ModelState.IsValid)
            {
                _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "ValuesController.cs", "Get"), CancellationToken.None);
                return new string[] { System.Net.HttpStatusCode.InternalServerError.ToString() };
            }
            try {
                var result = new string[] { "Environment Name", this._optionsAccessor?.Value?.EnvironmentName };
                _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Get values Successful", "ValuesController.cs", "Get"), CancellationToken.None);

                return result;
            }

            catch (Exception ex)
            {
               _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in ValuesController Get()", CancellationToken.None);

                string exceptionMsg = string.Format("Error,{0},{1}", ex.Message,ex.InnerException == null ? "" : ex.InnerException.Message);
                return new string[] { exceptionMsg };
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/values/5
        [HttpGet("{id}")]
        [SwaggerResponse(200, Type = typeof(string))]
        public string Get(int id)
        {
            return "value"+id.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
