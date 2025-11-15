using CommonSolution.Domain.Entities.Logging;

namespace CommonSolution.Domain.Interfaces.Logging
{
    public interface ILogHandler
    {
        string ProviderName { get; }
        Task LogAsync(LogEntry logEntry);
    }
}
