using System.Text.Json;

namespace CommonSolution.Abstractions.DTOs
{
    public class ApiSuccessResponse
    {
        public bool Success => true;
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public JsonElement? Payload { get; set; }

    }
}
