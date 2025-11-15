namespace CommonSolution.Domain.Entities.Logging
{
    public class LogEntry
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? StackTraceId { get; set; }
        public string? StackTrace { get; set; }
        public string? Route { get; set; }
        public string? Source { get; set; }
        public string? IpAddress { get; set; }
        public string? HttpMethod { get; set; }
        public string? RequestBody { get; set; }
        public string? ResponseBody { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime ResponseTime { get; set; }
        public TimeSpan Duration { get; set; }
        public int? CompanyId { get; set; }
        public int? BusinessUnitId { get; set; }
    }
}
