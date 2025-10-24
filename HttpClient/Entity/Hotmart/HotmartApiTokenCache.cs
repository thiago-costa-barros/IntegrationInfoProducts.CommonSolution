namespace CommonSolution.HttpClient.Entity.Hotmart
{
    public class HotmartApiTokenCache
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
