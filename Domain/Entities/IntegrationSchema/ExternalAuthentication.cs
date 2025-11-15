using System.ComponentModel.DataAnnotations.Schema;
using CommonSolution.Domain.Entities.Common;
using CommonSolution.Domain.Entities.Common.Enums;

namespace CommonSolution.Domain.Entities.IntegrationSchema
{
    [Table("ExternalAuthentication", Schema = "IntegrationSchema")]
    public class ExternalAuthentication : AuditableEntity
    {
        public int ExternalAuthenticationId { get; set; }

        public ExternalAuthenticationType AuthType { get; set; }
        public string? AuthKey { get; set; }

        public int CompanyId { get; set; }
        public int BusinessUnitId { get; set; }
    }
}
