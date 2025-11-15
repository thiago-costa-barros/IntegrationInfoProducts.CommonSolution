using CommonSolution.Domain.Entities.Common;
using CommonSolution.Domain.Entities.Logging;
using CommonSolution.Domain.Interfaces.Logging;
using Microsoft.Extensions.Options;
using System.Xml.Serialization;

namespace CommonSolution.Utils.Handlers
{
    public class XmlFileLogHandler : ILogHandler
    {
        private readonly string _xmlFilePath;
        private readonly string _xmlDefaultFileName;

        public XmlFileLogHandler(IOptions<AppSettings> config)
        {
            _xmlFilePath = config.Value.XmlFilePath ?? Path.Combine(AppContext.BaseDirectory, "Logs");
            _xmlDefaultFileName = config.Value.DefaultLogName ?? "DefaultLog_";
        }
        public string ProviderName => "Xml";

        public Task LogAsync(LogEntry logEntry)
        {
            try
            {
                Directory.CreateDirectory(_xmlFilePath);

                var fileName = $"{_xmlDefaultFileName}_{DateTime.Now:yyyy-MM-dd_HH}.xml";
                var fullPath = Path.Combine(_xmlFilePath, fileName);

                var json = System.Text.Json.JsonSerializer.Serialize(logEntry);
                File.AppendAllText(fullPath, json + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LOG ERROR] Falha ao salvar log XML: {ex.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
