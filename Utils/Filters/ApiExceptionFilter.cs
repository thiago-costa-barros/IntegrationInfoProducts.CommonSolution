using CommonSolution.Domain.Abstractions.DTOs;
using CommonSolution.Domain.Resources;
using CommonSolution.Utils.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace CommonSolution.Utils.Filters
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
            var apiMessage = new ApiMessage();

            _httpContextAccessor.HttpContext?.Items.TryAdd("Exception", exception);

            switch (exception)
            {
                case FluentValidation.ValidationException fluentValidation:
                    statusCode = StatusCodes.Status400BadRequest;
                    errorType = "ValidationException";
                    apiMessage.Value = string.Format(ExceptionMessages.FluentValidationException);
                    apiMessage.Details = fluentValidation.Errors.Select(e => e.ErrorMessage).ToList();
                    break;


                case ArgumentNullException:
                case ValidationException:
                case ArgumentException:
                case JsonException:
                    statusCode = StatusCodes.Status400BadRequest;
                    apiMessage.Value = exception.Message;
                    break;

                case KeyNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    apiMessage.Value = exception.Message;
                    break;

                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    apiMessage.Value = exception.Message;
                    break;

                case DbUpdateException ex:
                    statusCode = StatusCodes.Status422UnprocessableEntity;
                    errorType = "ApplicationException";
                    var detail = ex.InnerException?.Message ?? ex.Message;
                    apiMessage.Value = string.Format(ExceptionMessages.DatabaseException,detail);
                    break;

                case NotImplementedException:
                    statusCode = StatusCodes.Status501NotImplemented;
                    apiMessage.Value = exception.Message;
                    break;

                default:
                    apiMessage.Value = ExceptionMessageHelper.UnexpectedError();
                    break;
            }

            var response = new ApiErrorResponse
            {
                StatusCode = statusCode,
                ErrorType = errorType,
                TraceId = traceId,
                RequestTime = DateTime.UtcNow,
                Message = apiMessage,
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
