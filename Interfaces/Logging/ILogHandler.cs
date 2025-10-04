using CommonSolution.Entities.Logging;

namespace CommonSolution.Interfaces.Logging
{
    public interface ILogHandler
    {
        string ProviderName { get; }
        Task LogAsync(LogEntry logEntry);
    }
}
