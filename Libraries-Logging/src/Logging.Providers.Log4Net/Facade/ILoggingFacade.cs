using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Contracts.Models;
using Worldpay.LogicFacade.Contracts.Interfaces;

namespace Worldpay.Logging.Providers.Log4Net.Facade
{
    //interface ILoggingFacade
    //{
    //}

    public interface ILoggingFacade
    {
        Task<IFacadeResult> LogAsync(LogLevels logLevel, string message, CancellationToken cancellationToken, [CallerFilePath] string callerFile = "", [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = -1);
        Task<IFacadeResult> LogAsync(LogEntry logEntry, CancellationToken cancellationToken, [CallerFilePath] string callerFile = "", [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = -1);

        Task<IFacadeResult> LogExceptionAsync(Exception ex, string userName, LogLevels logLevel, string message, CancellationToken cancellationToken, [CallerFilePath] string callerFile = "", [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = -1);
    }
}
