using CommonSolution.Entities.Common.Enums;
using CommonSolution.Entities.CoreSchema;

namespace CommonSolution.Interfaces.DAOs
{
    /// <summary>
    /// Defines the data access operations for the ExternalAuthentication entity.
    /// Uses Entity Framework to execute queries directly against the database.
    /// </summary>
    public interface IExternalAuthenticationDAO
    {
        /// <summary>
        /// Fetches the company entity linked to the provided external authentication token.
        /// Executes the query using EF Core based on AuthType and AuthKey.
        /// </summary>
        /// <returns>The company entity if a match is found; otherwise, null.</returns>
        Task<Company?> GetCompanyByTokenAsync(string externalAuth, ExternalAuthenticationType type);
    }
}