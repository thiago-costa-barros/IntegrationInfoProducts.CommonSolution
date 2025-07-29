using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonSolution.DTOs
{
    public class ApiSuccessResponse
    {
        public bool Success => true;
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public JsonElement? Payload { get; set; }

    }
}
