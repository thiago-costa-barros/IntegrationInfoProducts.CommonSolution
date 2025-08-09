using CommonSolution.Entities.Common;
using CommonSolution.Entities.CoreSchema;
using CommonSolution.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.IntegrationSchema
{
    [Table("ExternalAuthentication", Schema = "IntegrationSchema")]
    public class ExternalAuthentication : AuditableEntity
    {
        public int ExternalAuthenticationId { get; set; }

        public ExternalAuthenticationType AuthType { get; set; }
        public string? AuthKey { get; set; }

        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public required Company Company { get; set; }
    }
}
