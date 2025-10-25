using CommonSolution.Entities.Common;
using System.Buffers;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.CoreSchema
{
    [Table("OperationBillet", Schema = "CoreSchema")]
    public class OperationBillet: AuditableEntity
    {
        public int OperationBilletId { get; set; }
        public DateTime OperationDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal PrincipalValue { get; set; }
        public OperationStatus Status { get; set; }
        public string? Barcode { get; set; }
        public string? BilletUrl { get; set; }
        public string? ConciliationId { get; set; }
        public int BusinessUnitId { get; set; }
    }
}
