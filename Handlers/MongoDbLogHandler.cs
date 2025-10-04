using CommonSolution.Entities.Logging;
using CommonSolution.Interfaces.Logging;

namespace CommonSolution.Handlers
{
    public class MongoDbLogHandler : ILogHandler
    {
        public string ProviderName => "MongoDb";

        public Task LogAsync(LogEntry logEntry)
        {
            throw new NotImplementedException();
        }
    }
}
