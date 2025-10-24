using CommonSolution.HttpClient.Entity;
using CommonSolution.HttpClient.Entity.Hotmart;

namespace CommonSolution.HttpClient.Interfaces
{
    public interface IHotmartApiClientService
    {
        Task<HotmartApiCredentialsResponse?> GetAccessToken(HotmartApiCredentials credentials, CancellationToken cancellationToken = default);
        Task<ApiResponse<HotmartApiResponse<HotmartProductApiResponse>?>> GetProductById(HotmartApiCredentials credentials, string paramId, CancellationToken cancellationToken = default);
        Task<ApiResponse<HotmartApiResponse<HotmartProductApiResponse>?>> GetProductOfferByCode(HotmartApiCredentials credentials, string paramId, string paramCode, CancellationToken cancellationToken = default);
        Task<ApiResponse<HotmartApiResponse<HotmartCouponApiResponse>?>> GetCouponsByProductId(HotmartApiCredentials credentials, string paramId, CancellationToken cancellationToken = default);
    }
}
