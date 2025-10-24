namespace CommonSolution.HttpClient.Interfaces
{
    public interface IHttpClientService
    {
        Task<TResponse?> MethodGet<TRequest,TResponse>(
            string url,
            TRequest? body,
            Dictionary<string, string>? headers = null,
            CancellationToken cancellationToken = default);
        Task<TResponse?> MethodPost<TRequest, TResponse>(
            string url,
            TRequest? body,
            Dictionary<string, string>? headers = null,
            CancellationToken cancellationToken = default);
    }
}
