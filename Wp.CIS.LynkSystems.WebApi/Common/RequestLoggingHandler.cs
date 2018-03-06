using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Models;

namespace Wp.CIS.LynkSystems.WebApi.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestLoggingHandler
    {
        private readonly ILoggingFacade _loggingFacade;
        private readonly IHostingEnvironment _env;
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="env"></param>
        /// <param name="loggingFacade"></param>
        public RequestLoggingHandler(RequestDelegate next, IHostingEnvironment env, ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _next = next;
            _env = env;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            string requestedQuery = string.Empty, methodName = string.Empty;
            bool ignoreLogging = false;
            var request = context.Request;
            string userName = "";// request.Headers["UserName"];
            try
            {
                if (request.Method.ToUpper() == "OPTIONS")
                {
                    ignoreLogging = true;
                }
                else
                {
                    userName = request.Headers["UserName"];
                }
                await _next.Invoke(context);
                if (!ignoreLogging)
                {
                    requestedQuery = request.QueryString.HasValue ? request.QueryString.Value : null;
                    methodName = request.Path.HasValue ? request.Path.Value.Split('/').LastOrDefault() : null;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Requested by " + userName + ", Requested Query: " + requestedQuery, callerMethod: methodName), CancellationToken.None);
                }
            }
            catch (Exception ex)
            {
                StackFrame frame;
                string fileName;
                string method;
                int srcIndex;

                frame = new StackTrace(ex, true).GetFrame(0);

                fileName = frame.GetFileName();
                if (string.IsNullOrEmpty(fileName))
                {
                    srcIndex = fileName.IndexOf("src");
                    if (srcIndex >= 0)
                    {
                        fileName = fileName.Substring(srcIndex + 3, fileName.Length - (srcIndex + 3));
                    }
                    else
                    {
                        fileName = fileName.Replace(_env.WebRootPath, "");
                    }
                }
                method = frame.GetMethod().DeclaringType.FullName + "." + ex.TargetSite.Name + "()";
                int lineNumber = frame.GetFileLineNumber();
                string stackTrace = ex.StackTrace == null ? "" : ex.StackTrace.TrimStart().Replace("\r\n at ", " called by " + context.User.Identity.Name);
                string exceptionMsg = string.Format("Error,{0},{1} {2}", ex.Message, //e.ToString()
                    ex.GetType().FullName, stackTrace);
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, exceptionMsg, fileName, method, lineNumber), CancellationToken.None);
            }
            finally
            {
                if (!ignoreLogging)
                {
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Completed Requested by " + userName + ", Requested Query: " + requestedQuery, callerMethod: methodName), CancellationToken.None);
                }
            }
        }
    }
}
