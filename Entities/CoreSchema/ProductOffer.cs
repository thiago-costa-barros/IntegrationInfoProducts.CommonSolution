using CommonSolution.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.CoreSchema
{
    [Table("ProductOffer", Schema = "CoreSchema")]
    public class ProductOffer: AuditableEntity
    {
        public int ProductOfferId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Externalidentifier { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int BusinessUnitId { get; set; }
        public int? ProductOfferCouponId { get; set; } = null;
    }
}
