using System.Text.Json.Serialization;

namespace CommonSolution.Abstractions.DTOs
{
    public class ApiErrorResponse
    {
        public bool Success => false;
        public int StatusCode { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorType { get; set; }
        public ApiMessage? Message { get; set; }
        public string? TraceId { get; set; }
        public DateTime RequestTime { get; set; }
    }

    public class ApiMessage
    {
        public string? Value { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Details { get; set; }
    }
}
