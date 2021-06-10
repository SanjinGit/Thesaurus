using Common.Thesaurus.Interfaces.Logger;
using NLog;

namespace Logger.Thesaurus.Manger
{
    /// <summary>
    /// Main project logger manager implementation.
    /// </summary>
    public class LoggerManager : ILoggerManager
    {
        // Default logger instance with name "ThesaurusLogger" configured in NLog.config file
        private static readonly ILogger _logger = LogManager.GetLogger("ThesaurusLogger");

        public LoggerManager()
        {
        }

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }

        public void LogWarn(string message)
        {
            _logger.Warn(message);
        }

        public void LogFatal(string message)
        {
            _logger.Fatal(message);
        }
    }
}
