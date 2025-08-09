using CommonSolution.Entities.Common;
using CommonSolution.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.IntegrationSchema
{
    [Table("ExternalWebhookReceiver", Schema = "IntegrationSchema")]
    public class ExternalWebhookReceiver : AuditableEntity
    {
        public int ExternalWebhookReceiverId { get; set; }
        public ExternalWebhookReceiverSourceType SourceType { get; set; }
        public ExternalWebhookReceiverStatus Status { get; set; }
        public int CompanyId { get; set; }
        public string? ExternalIdentifier { get; set; }
        public string? Payload { get; set; }
    }
}
