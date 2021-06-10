namespace Common.Thesaurus.Interfaces.Logger
{
    /// <summary>
    /// Main logger manager interface
    /// </summary>
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
        void LogFatal(string message);
    }
}
