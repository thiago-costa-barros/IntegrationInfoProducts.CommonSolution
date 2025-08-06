using CommonSolution.Entities;
using CommonSolution.Entities.Logging;
using CommonSolution.Interfaces.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CommonSolution.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
        private readonly IEnumerable<ILogHandler> _logHandlers;
        private readonly DefaultUserService _defaultUser;

        public RequestResponseLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestResponseLoggingMiddleware> logger,
            IEnumerable<ILogHandler> logHandlers,
            IOptions<DefaultUserService> defaultUser)
        {
            _next = next;
            _logger = logger;
            _logHandlers = logHandlers;
            _defaultUser = defaultUser.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            // Ignorar requisições do Swagger e arquivos estáticos
            var path = context.Request.Path.Value;

            if (path != null && (
                path.StartsWith("/swagger") ||
                path.EndsWith(".js") ||
                path.EndsWith(".css") ||
                path.EndsWith(".json") ||
                path.Contains("favicon.ico")))
            {
                await _next(context);
                return;
            }

            var requestTime = DateTime.UtcNow;

            // Captura do Request
            context.Request.EnableBuffering();
            string requestBody;
            using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
            {
                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            // Captura do Response
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            Exception? exception = null;
            if (context.Items.ContainsKey("Exception"))
                exception = context.Items["Exception"] as Exception;
            var companyId = context.Items.TryGetValue("CompanyId", out var companyIdObj)
                && int.TryParse(companyIdObj?.ToString(), out var parsedCompanyId)
                ? parsedCompanyId
                : (int?)null;

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var isSuccess = context.Response.StatusCode is >= 200 and < 300;

            var logEntry = new LogEntry
            {
                Success = isSuccess,
                StatusCode = context.Response.StatusCode,
                CompanyId = companyId,
                Message = TryExtractMessage(responseText),
                StackTraceId = context.TraceIdentifier,
                StackTrace = exception?.ToString(),
                Route = context.Request.Path,
                HttpMethod = context.Request.Method,
                RequestTime = requestTime,
                ResponseTime = DateTime.UtcNow,
                Duration = DateTime.UtcNow - requestTime,
                IpAddress = context.Connection.RemoteIpAddress?.ToString(),
                Source = _defaultUser.DefaultUserName,
                RequestBody = TryParse(requestBody),
                ResponseBody = TryParse(responseText)
            };

            foreach (var handler in _logHandlers)
                await handler.LogAsync(logEntry);

            await responseBody.CopyToAsync(originalBodyStream);

            if (exception != null)
                throw exception;
        }

        private static string? TryParse(string json)
        {
            try
            {
                using var jDoc = JsonDocument.Parse(json);
                return JsonSerializer.Serialize(jDoc, new JsonSerializerOptions { WriteIndented = false });
            }
            catch
            {
                return json; // já é uma string comum
            }
        }

        private static string? TryExtractMessage(string json)
        {
            try
            {
                var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("message", out var messageElement))
                {
                    if (messageElement.ValueKind == JsonValueKind.String)
                        return messageElement.GetString();

                    if (messageElement.TryGetProperty("value", out var valueProp))
                        return valueProp.GetString();
                }
            }
            catch { }
            return null;
        }
    }
}
