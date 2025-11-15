using CommonSolution.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Domain.Entities.CoreSchema
{
    [Table("ProductOffer", Schema = "CoreSchema")]
    public class ProductOffer: AuditableEntity
    {
        public int ProductOfferId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ExternalIdentifier { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int BusinessUnitId { get; set; }
    }
}
