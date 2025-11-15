using CommonSolution.Domain.Entities.Common;
using CommonSolution.Domain.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Domain.Entities.CoreSchema
{
    [Table("Product", Schema = "CoreSchema")]
    public class Product : AuditableEntity
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public ProductType Type { get; set; }
        public ProductStatus Status { get; set; }
        public ProductSourceType SourceType { get; set; }
        public int BusinessUnitId { get; set; }
        public string? Identifier { get; set; }
        public string? ExternalIdentifier { get; set; }
    }
}