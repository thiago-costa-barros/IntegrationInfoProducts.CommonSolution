using CommonSolution.Entities.Common;
using CommonSolution.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.CoreSchema
{
    [Table("OperationPurchaseCard", Schema = "Core")]
    public class OperationPurchaseCard: AuditableEntity
    {
        public int OperationPurchaseCardId { get; set; }
        public DateTime OperationDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal PrincipalValue { get; set; }
        public OperationStatus Status { get; set; }
        public string? RefuseMessage { get; set; }
        public int Installments { get; set; }
        public string? ConciliationId { get; set; }
        public int BusinessUnitId { get; set; }
    }
}
