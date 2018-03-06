using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Models;
using Worldpay.Logging.Providers.Log4Net.Extensions;
using Worldpay.Logging.Providers.Log4Net.Models;
using Worldpay.LogicFacade.Contracts.Enums;
using Worldpay.LogicFacade.Contracts.Interfaces;
using Worldpay.LogicFacade.Contracts.Models;
using System.Runtime.CompilerServices;
using System;

namespace Worldpay.Logging.Providers.Log4Net.Facade
{
    public class LoggingFacade : ILoggingFacade
    {
        public LoggingFacade(Log4NetConfig config)
        {
            LogManagerExtensions.LoadConfig(Assembly.GetEntryAssembly(), config);
        }

        public async Task<IFacadeResult> LogAsync(LogLevels logLevel, string message, CancellationToken cancellationToken = default
            , string callerFile = "", string callerMethod = "", int callerLine = -1)
        {
            return await ProcessIncoming(new LogEntry(logLevel, message));
        }

        public async Task<IFacadeResult> LogAsync(LogEntry logEntry, CancellationToken cancellationToken = default
            , string callerFile = "", string callerMethod = "", int callerLine = -1)
        {
            return await ProcessIncoming(logEntry);
        }
        
        private async Task<IFacadeResult> ProcessIncoming(LogEntry entry)
        {
            var log = LogManager.GetLogger(typeof(LogManager));
            
            var text = entry.ToLogString();
            
            switch (entry.LogLevel)
            {
                case LogLevels.All: log.Debug(text); break;
                case LogLevels.Debug: log.Debug(text); break;
                case LogLevels.Info: log.Info(text); break;
                case LogLevels.Warn: log.Warn(text); break;
                case LogLevels.Error: log.Error(text); break;
                case LogLevels.Fatal: log.Fatal(text); break;
                case LogLevels.Off: break;
            }

            return await Task.FromResult(new FacadeResult { StatusCode = FacadeStatusCodes.Ok });
        }

        public async Task<IFacadeResult> LogExceptionAsync(Exception ex, string userName, LogLevels logLevel, string message, CancellationToken cancellationToken, [CallerFilePath] string callerFile = "", [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = -1)
        {
            string stackTrace = $"{ex?.StackTrace} {ex?.StackTrace.TrimStart().Replace("\r\n at ", $"called by {userName}")}";
            string exceptionMsg = $"Message: {message} Error: Message: {ex?.Message} Type: {ex?.GetType()?.FullName} Stack Trace: {stackTrace} Inner Message: {ex?.InnerException?.Message} Inner stack trace: {ex?.InnerException?.StackTrace}";
            return await ProcessIncoming(new LogEntry(logLevel, exceptionMsg));
        }
    }
}
