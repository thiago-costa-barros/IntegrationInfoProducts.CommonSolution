using CommonSolution.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.CoreSchema
{
    [Table("Customer", Schema = "CoreSchema")]
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? TaxNumber { get; set; }
        public int BusinessUnitId { get; set; }
    }
}