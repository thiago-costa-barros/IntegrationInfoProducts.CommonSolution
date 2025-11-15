using CommonSolution.Domain.Entities.Common;
using CommonSolution.Domain.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Domain.Entities.CoreSchema
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
