using System.Text.Json.Serialization;

namespace CommonSolution.HttpClient.Entity.Hotmart
{
    public class HotmartApiResponse<T>
    {
        [JsonPropertyName("page_info")]
        public HotmartApiResponsePageInfo? PageInfo { get; set; }
        [JsonPropertyName("items")]
        public List<T>? Items { get; set; }
    }
    public class HotmartApiResponsePageInfo
    {
        [JsonPropertyName("next_page_token")]
        public bool NextPageToken { get; set; }
        [JsonPropertyName("prev_page_token")]
        public string? PrevPageToken { get; set; }
        [JsonPropertyName("results_per_page")]
        public int ResultsPerPage { get; set; }
    }
}
