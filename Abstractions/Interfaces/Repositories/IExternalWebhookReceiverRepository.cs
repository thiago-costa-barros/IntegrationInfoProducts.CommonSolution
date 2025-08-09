using CommonSolution.Entities.CoreSchema;
using CommonSolution.Entities.IntegrationSchema;

namespace CommonSolution.Interfaces.Repositories
{
    /// <summary>
    /// Defines the contract for repository operations related to ExternalWebhookReceiver.
    /// Responsible for delegating data access to DAO and mapping results to domain models.
    /// </summary>
    public interface IExternalWebhookReceiverRepository
    {
        Task InsertExternalWebhookAsync(ExternalWebhookReceiver externalWebhookReceiver);
        Task<ExternalWebhookReceiver?> GetExternalWebhookReceiverByIdenitifierAndCompanyId(string? identifier, Company company);
    }
}
