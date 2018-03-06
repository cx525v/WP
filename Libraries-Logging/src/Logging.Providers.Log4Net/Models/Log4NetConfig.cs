using System.Collections.Generic;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Providers.Log4Net.Validations;
using Worldpay.Validation.Contracts.Validations;

namespace Worldpay.Logging.Providers.Log4Net.Models
{
    public class Log4NetConfig : ModelValidation
    {
        public LogLevels Level { get; set; }
        public List<Log4NetAppenderConfig> Appenders { get; set; } = new List<Log4NetAppenderConfig>();

        public Log4NetConfig() 
            : base(new Log4NetConfigValidator())
        {

        }
    }
}
