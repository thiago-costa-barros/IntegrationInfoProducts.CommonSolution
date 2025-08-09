using CommonSolution.Entities.CoreSchema;
using CommonSolution.Entities.Common.Enums;

namespace CommonSolution.Interfaces.Repositories
{
    /// <summary>
    /// Defines the contract for repository operations related to external authentication.
    /// Responsible for delegating data access to DAO and mapping results to domain models.
    /// </summary>
    public interface IExternalAuthenticationRepository
    {
        /// <summary>
        /// Retrieves the company entity associated with the given external authentication token.
        /// Delegates query execution to the data access layer.
        /// </summary>
        /// <returns>The company entity associated with the token, or null if not found.</returns>
        Task<Company?> GetCompanyByTokenAsync(string? externalAuth, ExternalAuthenticationType externalAuthenticationType);
    }

}
