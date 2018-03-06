using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Providers.Log4Net.Enums;
using Worldpay.Logging.Providers.Log4Net.Validations;
using Worldpay.Validation.Contracts.Validations;

namespace Worldpay.Logging.Providers.Log4Net.Models
{
    /* Level Hierarchy
            ALL
            DEBUG
            INFO
            WARN
            ERROR
            FATAL
            OFF
    */

    public class Log4NetAppenderConfig : ModelValidation
    {
        public AppenderTypes? AppenderType { get; set; }
        public string ConversionPattern { get; set; }
        public LogLevels LevelMin { get; set; }
        public LogLevels LevelMax { get; set; }
        public bool? AppendToFile { get; set; }
        public string File { get; set; }
        public string DatePattern { get; set; }
        public int? MaxSizeRollBackups { get; set; }
        public bool? StaticLogFileName { get; set; }
        public RollingStyles RollingStyle { get; set;  }

        public Log4NetAppenderConfig() 
            : base(new Log4NetAppenderConfigValidator())
        {
        }
    }
}
