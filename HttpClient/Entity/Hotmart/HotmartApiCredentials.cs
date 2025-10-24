namespace CommonSolution.HttpClient.Entity.Hotmart
{
    public class HotmartApiCredentials
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string GrantType { get; set; } = "client_credentials";
        public string AccessToken { get; set; } = string.Empty;
        public string AuthBaseUrl { get; set; } = string.Empty;
        public string ApiBaseUrl { get; set; } = string.Empty;
    }
}
