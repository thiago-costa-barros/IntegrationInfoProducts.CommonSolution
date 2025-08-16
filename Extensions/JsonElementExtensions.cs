using System.Text.Json;

namespace CommonSolution.Extensions
{
    public static class JsonElementExtensions
    {
        public static T? MapToObject<T>(this JsonElement? element)
        {
            if (element == null || element.Value.ValueKind == JsonValueKind.Null)
                return default;

            return JsonSerializer.Deserialize<T>(element.Value.GetRawText(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
