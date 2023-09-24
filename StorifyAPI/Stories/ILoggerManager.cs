namespace Contracts
{
    public interface ILoggerManager
    {
        void LogInfo(string messege);
        void LogDebug(string messege);
        void LogWarn(string messege);
        void LogError(string messege);
    }
}