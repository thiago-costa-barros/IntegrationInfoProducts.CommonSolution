using CommonSolution.Entities.Common;
using CommonSolution.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.CoreSchema
{
    [Table("Operations", Schema = "CoreSchema")]
    public class Operation: AuditableEntity
    {
        public int OperationId { get; set; }
        public int ProductId { get; set; }
        public int ProductOfferId { get; set; }
        public OperationStatus Status { get; set; }
        public decimal PrincipalValue { get; set; }
        public OperationType OperationType { get; set; }
        public int EntityId { get; set; }
        public DateTime OperationDate { get; set; }
        public DateTime WarrantyDate { get; set; }
        public string? Identifier { get; set; }
        public string? ReceiptFileName { get; set; }
        public int BusinessUnitId { get; set; }
    }
}
