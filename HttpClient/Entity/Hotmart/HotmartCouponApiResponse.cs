using System.Text.Json.Serialization;

namespace CommonSolution.HttpClient.Entity.Hotmart
{
    public class HotmartCouponApiResponse
    {
        public class CouponItem
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }
            [JsonPropertyName("start_date")]
            public int StartDate { get; set; }
            [JsonPropertyName("status")]
            public string? Status { get; set; }
            [JsonPropertyName("time_zone")]
            public CouponTimeZone? TimeZone { get; set; }
            [JsonPropertyName("active")]
            public bool Active { get; set; }
            [JsonPropertyName("discount")]
            public decimal Discount { get; set; }
            [JsonPropertyName("coupon_code")]
            public string? CouponCode { get; set; }
        }
        public class CouponTimeZone
        {
            [JsonPropertyName("description")]
            public string? Description { get; set; }
            [JsonPropertyName("id")]
            public int Id { get; set; }
            [JsonPropertyName("name")]
            public string? Name { get; set; }
            [JsonPropertyName("offset")]
            public int Offset { get; set; }
        }
    }
}
