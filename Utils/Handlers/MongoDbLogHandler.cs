using CommonSolution.Domain.Entities.Logging;
using CommonSolution.Domain.Interfaces.Logging;

namespace CommonSolution.Utils.Handlers
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
