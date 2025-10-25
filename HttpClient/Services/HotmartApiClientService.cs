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

        public async Task<ApiResponse<HotmartApiResponse<HotmartProductApiResponse>?>> GetProductById(HotmartApiCredentials credentials, string paramId, CancellationToken cancellationToken = default)
        {
            HotmartApiCredentialsResponse? tokenResponse = await GetAccessToken(credentials, cancellationToken);
            if (tokenResponse is null || string.IsNullOrWhiteSpace(tokenResponse.AccessToken))
                throw new InvalidOperationException("Não foi possível obter o token de acesso para a API Hotmart.");

            string url = $"{credentials.ApiBaseUrl}/products/api/v1/products?id={paramId}";
            var headers = new Dictionary<string, string>
            {
                ["Authorization"] = $"Bearer {tokenResponse.AccessToken}"
            };
            try
            {
                ApiResponse<HotmartApiResponse<HotmartProductApiResponse>?>? httpClientResponse = await _httpClientService.MethodGet<object, ApiResponse<HotmartApiResponse<HotmartProductApiResponse>?>>(url, null, headers, cancellationToken);
                return httpClientResponse!;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao consultar produtos na API Hotmart: {ex.Message}", ex);
            }
        }

        public async Task<ApiResponse<HotmartApiResponse<HotmartProductOfferApiResponse>?>> GetProductOfferByCode(HotmartApiCredentials credentials, string paramId, string paramCode, CancellationToken cancellationToken = default)
        {
            HotmartApiCredentialsResponse? tokenResponse = await GetAccessToken(credentials, cancellationToken);
            if (tokenResponse is null || string.IsNullOrWhiteSpace(tokenResponse.AccessToken))
                throw new InvalidOperationException("Não foi possível obter o token de acesso para a API Hotmart.");

            string url = $"{credentials.ApiBaseUrl}/products/api/v1/products/{paramId}/offers";
            var headers = new Dictionary<string, string>
            {
                ["Authorization"] = $"Bearer {tokenResponse.AccessToken}"
            };
            try
            {
                ApiResponse<HotmartApiResponse<HotmartProductOfferApiResponse>?>? httpClientResponse = await _httpClientService.MethodGet<object, ApiResponse<HotmartApiResponse<HotmartProductOfferApiResponse>?>>(url, null, headers, cancellationToken);
                if (httpClientResponse?.Data?.Items is not null)
                {
                    FilterItems(httpClientResponse.Data, offer => offer.Code == paramCode);
                }

                return httpClientResponse!;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao consultar produtos na API Hotmart: {ex.Message}", ex);
            }
        }
        public async Task<ApiResponse<HotmartApiResponse<HotmartProductOfferApiResponse>?>> GetAllProductOffers(HotmartApiCredentials credentials, string paramId, CancellationToken cancellationToken = default)
        {
            HotmartApiCredentialsResponse? tokenResponse = await GetAccessToken(credentials, cancellationToken);
            if (tokenResponse is null || string.IsNullOrWhiteSpace(tokenResponse.AccessToken))
                throw new InvalidOperationException("Não foi possível obter o token de acesso para a API Hotmart.");

            string url = $"{credentials.ApiBaseUrl}/products/api/v1/products/{paramId}/offers";
            var headers = new Dictionary<string, string>
            {
                ["Authorization"] = $"Bearer {tokenResponse.AccessToken}"
            };
            try
            {
                ApiResponse<HotmartApiResponse<HotmartProductOfferApiResponse>?>? httpClientResponse = await _httpClientService.MethodGet<object, ApiResponse<HotmartApiResponse<HotmartProductOfferApiResponse>?>>(url, null, headers, cancellationToken);
                
                return httpClientResponse!;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao consultar produtos na API Hotmart: {ex.Message}", ex);
            }
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
        private static void FilterItems<T>(HotmartApiResponse<T>? response, Func<T, bool> predicate)
        {
            if (response?.Items is null) return;
            response.Items = response.Items.Where(predicate).ToList();
        }

    }
}
