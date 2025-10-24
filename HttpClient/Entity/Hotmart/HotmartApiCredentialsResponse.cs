﻿using System.Text.Json.Serialization;

namespace CommonSolution.HttpClient.Entity.Hotmart
{
    public class HotmartApiCredentialsResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = string.Empty;
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonPropertyName("scope")]
        public string Scope { get; set; } = string.Empty;
        [JsonPropertyName("jti")]
        public string Jti { get; set; } = string.Empty;
    }
}
