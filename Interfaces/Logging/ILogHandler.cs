using CommonSolution.Entities.Logging;

namespace CommonSolution.Interfaces.Logging
{
    public interface ILogHandler
    {
        Task LogAsync(LogEntry logEntry);
    }
}
