using System.Text.Json.Serialization;

namespace CommonSolution.HttpClient.Entity.Hotmart
{
    public class HotmartProductApiResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("ucode")]
        public string? Ucode { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("created_at")]
        public long CreatedAt { get; set; }
        [JsonPropertyName("format")]
        public string? Format { get; set; }
        [JsonPropertyName("is_subscription")]
        public bool IsSubscription { get; set; }
        [JsonPropertyName("warranty_period")]
        public int WarrantyPeriod { get; set; }
    }
}
