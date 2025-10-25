using System.Text.Json.Serialization;

namespace CommonSolution.HttpClient.Entity.Hotmart
{
    public class HotmartProductOfferApiResponse
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("price")]
        public HotmartProductOfferPriceObject? Price { get; set; }
        [JsonPropertyName("payment_mode")]
        public string? PaymentMode { get; set; }
        [JsonPropertyName("is_currency_conversion_enabled")]
        public bool IsCurrencyConversionEnabled { get; set; }
        [JsonPropertyName("is_smart_recovery_enabled")]
        public bool IsSmartRecoveryEnabled { get; set; }
        [JsonPropertyName("is_main_offer")]
        public bool IsMainOffer { get; set; }
    }
    public class  HotmartProductOfferPriceObject
    {
        [JsonPropertyName("value")]
        public decimal Value { get; set; }
        [JsonPropertyName("currency_code")]
        public string? CurrencyCode { get; set; }
    }
}
