using CommonSolution.HttpClient.Entity;
using CommonSolution.HttpClient.Entity.Hotmart;
using CommonSolution.HttpClient.Interfaces;
using System.Collections.Concurrent;

namespace CommonSolution.HttpClient.Services
{
    public class HotmartApiClientService : IHotmartApiClientService
    {
        private readonly IHttpClientService _httpClientService;
        private static readonly ConcurrentDictionary<string, HotmartApiTokenCache> _tokenCache = new();
        private static readonly SemaphoreSlim _lock = new(1, 1);
        public HotmartApiClientService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }
        public async Task<HotmartApiCredentialsResponse?> GetAccessToken(HotmartApiCredentials credentials, CancellationToken cancellationToken = default)
        {
            string clientId = credentials.ClientId;
            string clientSecret = credentials.ClientSecret;

            if (_tokenCache.TryGetValue(clientId, out HotmartApiTokenCache? cachedToken))
            {
                var cachedResponse = ValidateCachedHotmartApiToken(clientId, clientSecret, cachedToken);
                if (cachedResponse is not null)
                    return cachedResponse;
            }

            await _lock.WaitAsync(cancellationToken);
            try
            {
                if (_tokenCache.TryGetValue(clientId, out HotmartApiTokenCache? checkCachedToken))
                {
                    var checkResponse = ValidateCachedHotmartApiToken(clientId, clientSecret, checkCachedToken);
                    if (checkResponse is not null)
                        return checkResponse;
                }

                var basicAuth = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

                var headers = new Dictionary<string, string>
                {
                    ["Authorization"] = $"Basic {basicAuth}",
                    ["Content-Type"] = "application/json"
                };
                var url = $"{credentials.AuthBaseUrl}/security/oauth/token" +
                  $"?grant_type=client_credentials" +
                  $"&client_id={clientId}" +
                  $"&client_secret={clientSecret}";
                var body = new Dictionary<string, string> { };
                HotmartApiCredentialsResponse? httpClientResponse = await _httpClientService.MethodPost<Dictionary<string, string>, HotmartApiCredentialsResponse>(url, body, headers, cancellationToken);

                if (httpClientResponse is not null && !string.IsNullOrWhiteSpace(httpClientResponse.AccessToken))
                {
                    DateTime expiration = DateTime.UtcNow.AddSeconds(httpClientResponse.ExpiresIn);
                    HotmartApiTokenCache newCache = new HotmartApiTokenCache
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret,
                        AccessToken = httpClientResponse.AccessToken,
                        ExpirationDate = expiration,
                    };

                    _tokenCache.AddOrUpdate(clientId, newCache, (_, _) => newCache);
                    return httpClientResponse;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao autenticar na API Hotmart: {ex.Message}", ex);
            }
            finally
            {
                _lock.Release();
            }
        }

        public Task<ApiResponse<HotmartApiResponse<HotmartProductApiResponse>?>> GetProductById(HotmartApiCredentials credentials, string paramId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<HotmartApiResponse<HotmartProductApiResponse>?>> GetProductOfferByCode(HotmartApiCredentials credentials, string paramId, string paramCode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<HotmartApiResponse<HotmartCouponApiResponse>?>> GetCouponsByProductId(HotmartApiCredentials credentials, string paramId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        private static HotmartApiCredentialsResponse? ValidateCachedHotmartApiToken(string clientId, string clientSecret, HotmartApiTokenCache? cachedToken)
        {
            if (cachedToken is null)
                return null;

            bool isValidToken = cachedToken.ExpirationDate > DateTime.UtcNow.AddMinutes(-1) &&
                                cachedToken.ClientId == clientId &&
                                cachedToken.ClientSecret == clientSecret;
            return isValidToken
                ? new HotmartApiCredentialsResponse
                {
                    AccessToken = cachedToken.AccessToken,
                    ExpiresIn = (int)(cachedToken.ExpirationDate - DateTime.UtcNow).TotalSeconds
                }
                : null;
        }
    }
}
