using CommonSolution.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.CoreSchema
{
    [Table("Company", Schema = "CoreSchema")]
    public class Company : AuditableEntity
    {
        public int CompanyId { get; set; }
        public string? Name { get; set; }
        public string? TaxNumber { get; set; }
        public CompanyType Type { get; set; }
        public CompanyStatus Status { get; set; }
    }
}
