using CommonSolution.DTOs;
using CommonSolution.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
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
            var statusCode = StatusCodes.Status500InternalServerError;
            var errorType = exception.GetType().Name;
            string? singleMessage = null;
            List<string>? multipleMessages = null;

            switch (exception)
            {
                case FluentValidation.ValidationException fluentValidation:
                    statusCode = StatusCodes.Status400BadRequest;
                    errorType = "Validation Error";
                    multipleMessages = fluentValidation.Errors.Select(e => e.ErrorMessage).ToList();
                    break;


                case ArgumentNullException:
                case ValidationException:
                case ArgumentException:
                case JsonException:
                    statusCode = StatusCodes.Status400BadRequest;
                    singleMessage = exception.Message;
                    break;

                case KeyNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    singleMessage = exception.Message;
                    break;

                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    singleMessage = ExceptionMessageHelper.UnauthorizedAccess();
                    break;

                case NotImplementedException:
                    statusCode = StatusCodes.Status501NotImplemented;
                    singleMessage = exception.Message;
                    break;

                default:
                    singleMessage = ExceptionMessageHelper.UnexpectedError();
                    break;
            }

            var response = new ApiErrorResponse
            {
                StatusCode = statusCode,
                ErrorType = errorType,
                TraceId = traceId,
                RequestTime = DateTime.UtcNow,
                Message = singleMessage,
                Messages = multipleMessages,
                Details = _env.IsDevelopment() ? exception.StackTrace : null
            };

            _logger.LogError(exception, "Unhandled exception caught. TraceId: {TraceId}", traceId);

            context.Result = new ObjectResult(response)
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }
    }
}
