using CommonSolution.Domain.Entities.Common;
using CommonSolution.Domain.Entities.Logging;
using CommonSolution.Domain.Interfaces.Logging;
using CommonSolution.Utils.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CommonSolution.Utils.Middleware
{
    public class ApiLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiLoggingMiddleware> _logger;
        private readonly IEnumerable<ILogHandler> _logHandlers;
        private readonly DefaultUserService _defaultUser;
        private readonly IOptions<LoggingProvidersOptions> _loggingProviders;

        public ApiLoggingMiddleware(
            RequestDelegate next,
            ILogger<ApiLoggingMiddleware> logger,
            IEnumerable<ILogHandler> logHandlers,
            IOptions<DefaultUserService> defaultUser,
            IOptions<LoggingProvidersOptions> loggingProviders)
        {
            _next = next;
            _logger = logger;
            _logHandlers = logHandlers;
            _defaultUser = defaultUser.Value;
            _loggingProviders = loggingProviders;
        }

        public async Task Invoke(HttpContext context)
        {
            // Ignorar requisições do Swagger e arquivos estáticos
            bool ignoredRequest = IgnoredRequests(context);

            if (ignoredRequest)
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

            AuditableEntityLog auditableEntityLog = MappingAuditableEntitiesLogs(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var isSuccess = context.Response.StatusCode is >= 200 and < 300;

            var logEntry = new LogEntry
            {
                Success = isSuccess,
                StatusCode = context.Response.StatusCode,
                CompanyId = auditableEntityLog.CompanyId,
                BusinessUnitId = auditableEntityLog.BusinessUnitId,
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
            {
                var shouldLog = handler switch
                {
                    XmlFileLogHandler => !isSuccess && context.Response.StatusCode >= 500,

                    _ => _loggingProviders.Value.EnabledProviders.Contains(handler.ProviderName)
                };

                if (!shouldLog)
                    continue;

                try
                {
                    await handler.LogAsync(logEntry);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Erro ao enviar log para handler {Handler}", handler.GetType().Name);

                    try
                    {
                        var xmlHandler = _logHandlers.FirstOrDefault(h => h is XmlFileLogHandler);
                        if (xmlHandler != null)
                            await xmlHandler.LogAsync(logEntry);
                    }
                    catch (Exception xmlEx)
                    {
                        _logger.LogError(xmlEx, "Falha ao registrar log em XML de fallback");
                    }
                }
            }


            await responseBody.CopyToAsync(originalBodyStream);

            if (exception != null)
                throw exception;
        }

        private static bool IgnoredRequests(HttpContext context)
        {
            var path = context.Request.Path.Value;

            if (path != null && (
                path.StartsWith("/swagger") ||
                path.EndsWith(".js") ||
                path.EndsWith(".css") ||
                path.EndsWith(".json") ||
                path.Contains("favicon.ico")))
            {
                return true;
            }

            return false;
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

        private static AuditableEntityLog MappingAuditableEntitiesLogs(HttpContext context)
        {
            var companyId = context.Items.TryGetValue("CompanyId", out var companyIdObj)
                && int.TryParse(companyIdObj?.ToString(), out var parsedCompanyId)
                ? parsedCompanyId
                : (int?)null;

            var businessUnitId = context.Items.TryGetValue("BusinessUnitId", out var businessUnitIdObj)
                && int.TryParse(businessUnitIdObj?.ToString(), out var parsedBusinessUnitId)
                ? parsedBusinessUnitId
                : (int?)null;

            AuditableEntityLog result = new AuditableEntityLog
            {
                CompanyId = companyId ?? 0,
                BusinessUnitId = businessUnitId ?? 0
            };
            return result;
        }
    }
}
