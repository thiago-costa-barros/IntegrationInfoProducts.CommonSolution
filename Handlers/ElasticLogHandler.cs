using CommonSolution.Entities.Logging;
using CommonSolution.Interfaces.Logging;
using Microsoft.Extensions.Options;
using Nest;

namespace CommonSolution.Handlers
{
    public class ElasticLogHandler : ILogHandler
    {
        private readonly IElasticClient _client;
        private readonly string _index;

        public ElasticLogHandler(IOptions<LoggingOptions> options)
        {
            var config = options.Value;

            _index = config.IndexName;

            var settings = new ConnectionSettings(new Uri(config.Url))
                .DefaultIndex(_index);

            _client = new ElasticClient(settings);
        }

        public async Task LogAsync(LogEntry logEntry)
        {
            await _client.IndexDocumentAsync(logEntry);
        }
    }
}
