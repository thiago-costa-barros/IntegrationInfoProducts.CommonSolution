using CommonSolution.HttpClient.Interfaces;
using System.Text.Json;

namespace CommonSolution.HttpClient.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public HttpClientService(System.Net.Http.HttpClient httpClient, JsonSerializerOptions jsonOptions)
        {
            _httpClient = httpClient;
            _jsonOptions = jsonOptions;
        }
        public async Task<TResponse?> MethodGet<TRequest, TResponse>(string url, TRequest? body, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = body is not null
                    ? new StringContent(JsonSerializer.Serialize(body, _jsonOptions), System.Text.Encoding.UTF8, "application/json")
                    : null
            };
            AddHeaders(requestMessage, headers);

            var response = await _httpClient.SendAsync(requestMessage, cancellationToken);
            return await HandleResponse<TResponse>(response);
        }

        public async Task<TResponse?> MethodPost<TRequest, TResponse>(string url, TRequest? body, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        {
            {
                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = body is not null
                        ? new StringContent(JsonSerializer.Serialize(body, _jsonOptions), System.Text.Encoding.UTF8, "application/json")
                        : null
                };
                AddHeaders(requestMessage, headers);

                var response = await _httpClient.SendAsync(requestMessage, cancellationToken);
                return await HandleResponse<TResponse>(response);
            }
        }
        private void AddHeaders(HttpRequestMessage request, Dictionary<string, string>? headers)
        {
            if (headers is null) return;
            foreach (var header in headers)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }
        private async Task<TResponse?> HandleResponse<TResponse>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Erro HTTP {response.StatusCode}: {content}");
            }

            if (string.IsNullOrWhiteSpace(content))
                return default;

            return JsonSerializer.Deserialize<TResponse>(content, _jsonOptions);
        }
    }
}
