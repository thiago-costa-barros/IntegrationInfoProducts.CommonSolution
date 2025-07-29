using CommonSolution.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace CommonSolution.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var traceId = _httpContextAccessor.HttpContext?.TraceIdentifier;

            var statusCode = exception switch
            {
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ValidationException => StatusCodes.Status400BadRequest,
                ArgumentNullException => StatusCodes.Status400BadRequest,
                ArgumentException => StatusCodes.Status400BadRequest,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                NotImplementedException => StatusCodes.Status501NotImplemented,
                JsonException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new ApiErrorResponse
            {
                StatusCode = statusCode,
                ErrorType = exception.GetType().Name,
                Message = exception.Message,
                RequestTime = DateTime.UtcNow,
                TraceId = traceId,
                Details = _env.IsDevelopment() ? exception.StackTrace : null
            };

            _logger.LogError(exception, "Unhandled exception caught. TraceId: {TraceId}", traceId);

            context.Result = new ObjectResult(response)
            {
                StatusCode = statusCode
            };
        }
    }
}
