using CommonSolution.DTOs;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace CommonSolution.Filters
{
    public class ApiSuccessResponseFilter
    {
        public static ApiSuccessResponse CreateSuccessResponse(string message, JsonElement payload)
        {
            ApiSuccessResponse response = new ApiSuccessResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Message = message,
                Payload = JsonSerializer.SerializeToElement(payload)
            };

            return response;
        }
    }
}
