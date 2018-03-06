using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Models;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model.Tree;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// the api to manipulate xml validation, xml modification and primeNg tree ...
    /// </summary>
    [Produces("application/json")]
    [Route("api/xml")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class XmlController : Controller
    {
        private readonly IDistributedCache _cache;
      //  private readonly IEPSTableApi _epsTableApi;
        private readonly IStringLocalizer<XmlController> _localizer;
     //   private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;
        private readonly IXmlApi _xmlApi;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="cache"></param>
      /// <param name="localizer"></param>
      /// <param name="loggingFacade"></param>
      /// <param name="xmlApi"></param>
        public XmlController(IDistributedCache cache, 
             IStringLocalizer<XmlController> localizer,           
             ILoggingFacade loggingFacade,
             IXmlApi xmlApi)
        {
            _xmlApi = xmlApi;
            _cache = cache;       
            _localizer = localizer;         
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Xml Controller", "XmlController.cs", "XmlController"), CancellationToken.None);
        }

        /// <summary>
        /// get table information from schema, such as table name ...
        /// </summary>
        /// <param name="schemaDef"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getTableSchema")]
        public async Task<IActionResult> GetTableSchema([FromBody]string schemaDef)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting GetTableSchema ", "XmlController.cs", "GetTableSchema"), CancellationToken.None);
                TableProperty result = await Task.Run(() => _xmlApi.GetTableProperty(schemaDef));
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in XmlController GetTableSchema() ", CancellationToken.None);
                return BadRequest(ex);
            }
        }



        /// <summary>
        /// Update default xml values
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateDefaultXml")]
        public async Task<IActionResult> EPSUpdatePetroTableDefaultXml([FromBody]UpdateXmlModel data)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting EPSUpdatePetroTableDefaultXml ", "XmlController.cs", "EPSUpdatePetroTableDefaultXml"), CancellationToken.None);
                var result = Task.Run(() =>
                  _xmlApi.Update(data)
                );
                return Ok(await result);
            }
            catch (System.Exception ex)
            {
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in XmlController EPSUpdatePetroTableDefaultXml() ", CancellationToken.None);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// validate xml schema
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("validateXml")]
        public async Task<IActionResult> Validate([FromBody] ValidateXmlModel data)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Validate ", "XmlController.cs", "Validate"), CancellationToken.None);
                var result = Task.Run(() =>
                  _xmlApi.IsValid(data.dictionaries, data.xsd, data.xml)               
                );
                return Ok(await result);
            }
            catch (System.Exception ex)
            {
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in XmlController Validate() ", CancellationToken.None);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// xml string to convert to PrimeNG tree data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gettreenode")]
        public async Task<IActionResult> GetTreeNode([FromBody] DefaultXmlModel data)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting GetTreeNode ", "XmlController.cs", "GetTreeNode"), CancellationToken.None);
                var result = Task.Run(() =>
                   _xmlApi.ConvertToTree(data.defaultXml)
                 );
                return Ok(await result);
            }
            catch (System.Exception ex)
            {
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in XmlController GetTreeNode() ", CancellationToken.None);
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("mapping")]
        public async Task<IActionResult> GetMappingFromXml([FromBody]string xml)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting GetMappingFromXml ", "XmlController.cs", "GetMappingFromXml"), CancellationToken.None);
                var result = Task.Run(() =>
                  _xmlApi.GetMappingFromXml(xml)
                );
                return Ok(await result);
            }
            catch (System.Exception ex)
            {
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in XmlController GetMappingFromXml() ", CancellationToken.None);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("mappings")]
        public async Task<IActionResult> GetMappingsFromXml([FromBody]string xml)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting GetMappingsFromXml ", "XmlController.cs", "GetMappingsFromXml"), CancellationToken.None);
                var result = Task.Run(() =>
                  _xmlApi.GetMappingsFromXml(xml)
                );
                return Ok(await result);
            }
            catch (System.Exception ex)
            {
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in XmlController GetMappingsFromXml() ", CancellationToken.None);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("petrotable")]
        public async Task<IActionResult> GetPetroTableFromXml([FromBody]string xml)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting GetPetroTableFromXml ", "XmlController.cs", "GetPetroTableFromXml"), CancellationToken.None);
                var result = Task.Run(() =>
                  _xmlApi.GetTableFromXml(xml)
                );
                return Ok(await result);
            }
            catch (System.Exception ex)
            {
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in XmlController GetPetroTableFromXml() ", CancellationToken.None);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("petrotables")]
        public async Task<IActionResult> GetPetroTablesFromXml([FromBody]string xml)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting GetPetroTablesFromXml ", "XmlController.cs", "GetPetroTablesFromXml"), CancellationToken.None);
                var result = Task.Run(() =>
                  _xmlApi.GetTablesFromXml(xml)
                );
                return Ok(await result);
            }
            catch (System.Exception ex)
            {
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in XmlController GetPetroTablesFromXml() ", CancellationToken.None);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("versionaudit")]
        public async Task<IActionResult> GetVersionAuditFromXml([FromBody]string xml)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting GetVersionAuditFromXml ", "XmlController.cs", "GetVersionAuditFromXml"), CancellationToken.None);

                var result = Task.Run(() =>
                  _xmlApi.GetVersionFromXMl(xml)
                );
                return Ok(await result);
            }
            catch (System.Exception ex)
            {
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in XmlController GetVersionAuditFromXml() ", CancellationToken.None);
                return BadRequest(ex);
            }
        }
    }   
}
