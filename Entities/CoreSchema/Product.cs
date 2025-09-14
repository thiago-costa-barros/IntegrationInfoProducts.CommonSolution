using CommonSolution.Entities.Common;
using CommonSolution.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.CoreSchema
{
    public class Product
    {
        [Table("Product", Schema = "CoreSchema")]
        public class CompanyBranch : AuditableEntity
        {
            public int ProductId { get; set; }
            public string? Name { get; set; }
            public ProductType Type { get; set; }
            public ProductStatus Status { get; set; }
            public ExternalWebhookReceiverSourceType SourceType { get; set; }
            public int BusinessUnitId { get; set; }
        }
    }
}
