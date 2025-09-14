using CommonSolution.Entities.Common;
using CommonSolution.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.CoreSchema
{
    [Table("Company", Schema = "CoreSchema")]
    public class Company : AuditableEntity
    {
        public int CompanyId { get; set; }
        public string? Name { get; set; }
        public int PersonId { get; set; }
        public CompanyType Type { get; set; }
        public CompanyStatus Status { get; set; }
    }
}
