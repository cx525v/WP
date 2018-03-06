using System.Reflection;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Providers.Log4Net.Enums;
using Worldpay.Logging.Providers.Log4Net.Models;

namespace Worldpay.Logging.Providers.Log4Net.Extensions
{
    public static class LogManagerExtensions
    {
        //If you need to support more log4net configuration, simply add them to this class and the Log4Net config entries in appSettings

        public static void LoadConfig(Assembly assembly, Log4NetConfig config)
        {
            var hierarchy = (Hierarchy) LogManager.GetRepository(assembly);
            hierarchy.Root.RemoveAllAppenders();

            foreach (var appender in config.Appenders)
            {
                if (appender.AppenderType == AppenderTypes.Console)
                    hierarchy.Root.AddAppender(LoadConsoleAppender(appender));
                else if (appender.AppenderType == AppenderTypes.RollingFile)
                    hierarchy.Root.AddAppender(LoadRollingFileAppender(appender));
            }

            hierarchy.Root.Level = GetLevel(config.Level);
            hierarchy.Configured = true;
        }

        private static IAppender LoadRollingFileAppender(Log4NetAppenderConfig appenderConfig)
        {
            var roller = new RollingFileAppender();

            if (!string.IsNullOrWhiteSpace(appenderConfig.ConversionPattern))
            {
                var patternLayout = new PatternLayout {ConversionPattern = appenderConfig.ConversionPattern};
                patternLayout.ActivateOptions();
                roller.Layout = patternLayout;
            }
            
            roller.File = appenderConfig.File;

            if (appenderConfig.AppendToFile != null)
                roller.AppendToFile = appenderConfig.AppendToFile.Value;

            if (appenderConfig.MaxSizeRollBackups != null)
                roller.MaxSizeRollBackups = (int) appenderConfig.MaxSizeRollBackups;

            if (appenderConfig.StaticLogFileName != null)
                roller.StaticLogFileName = (bool) appenderConfig.StaticLogFileName;

            if (appenderConfig.RollingStyle != RollingStyles.NotSet)
                roller.RollingStyle = GetRollingMode(appenderConfig.RollingStyle);

            if (!string.IsNullOrWhiteSpace(appenderConfig.DatePattern))
                roller.DatePattern = appenderConfig.DatePattern;

            var rangeFilter = GetLevelRange(appenderConfig);
            if (rangeFilter != null)
                roller.AddFilter(rangeFilter);

            roller.ActivateOptions();
            return roller;
        }

        private static LevelRangeFilter GetLevelRange(Log4NetAppenderConfig appenderConfig)
        {
            var rangeFilter = new LevelRangeFilter
            {
                LevelMin = GetLevel(appenderConfig.LevelMin),
                LevelMax = GetLevel(appenderConfig.LevelMax)
            };
            return rangeFilter;
        }

        private static Level GetLevel(LogLevels logLevel)
        {
            switch (logLevel)
            {
                case LogLevels.Debug : return Level.Debug;
                case LogLevels.Error: return Level.Error;
                case LogLevels.Fatal: return Level.Fatal;
                case LogLevels.Warn: return Level.Warn;
                case LogLevels.Off: return Level.Off;
                case LogLevels.Info: return Level.Info;
                case LogLevels.All: return Level.All;
                default: return Level.Off;
            }
        }
   
        private static IAppender LoadConsoleAppender(Log4NetAppenderConfig appenderConfig)
        {
            var appender = new ConsoleAppender();

            if (!string.IsNullOrWhiteSpace(appenderConfig.ConversionPattern))
            {
                var patternLayout = new PatternLayout { ConversionPattern = appenderConfig.ConversionPattern };
                patternLayout.ActivateOptions();
                appender.Layout = patternLayout;
            }

            appender.ActivateOptions();
            return appender;
        }

        private static RollingFileAppender.RollingMode GetRollingMode(RollingStyles rollingStyle)
        {
            switch (rollingStyle)
            {
                case RollingStyles.Composite: return RollingFileAppender.RollingMode.Date;
                case RollingStyles.Date: return RollingFileAppender.RollingMode.Composite;
                case RollingStyles.Once: return RollingFileAppender.RollingMode.Once;
                default: return RollingFileAppender.RollingMode.Size;
            }
        }

    }
}
