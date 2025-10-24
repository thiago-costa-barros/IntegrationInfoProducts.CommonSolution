using CommonSolution.Entities.Common;
using CommonSolution.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.CoreSchema
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