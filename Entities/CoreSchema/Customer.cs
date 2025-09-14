using CommonSolution.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.CoreSchema
{
    public class Customer
    {
        [Table("Customer", Schema = "CoreSchema")]
        public class CompanyBranch : AuditableEntity
        {
            public int CustomerId { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? TaxNumber { get; set; }
            public int BusinessUnitId { get; set; }
        }
    }
}
