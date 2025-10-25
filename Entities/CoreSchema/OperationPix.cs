using CommonSolution.Entities.Common;
using CommonSolution.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.CoreSchema
{
    [Table("OperationPix", Schema = "CoreSchema")]
    public class OperationPix: AuditableEntity
    {
        public int OperationPixId { get; set; }
        public DateTime OperationDate { get; set; }
        public DateTime? TransferDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal PrincipalValue { get; set; }
        public OperationStatus Status { get; set; }
        public string? HashCode { get; set; }
        public string? QrCodeImageUrl { get; set; }
        public string? ConciliationId { get; set; }
        public int BusinessUnitId { get; set; }
    }
}
