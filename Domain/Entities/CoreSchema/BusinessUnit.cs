using CommonSolution.Domain.Entities.Common;
using CommonSolution.Domain.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Domain.Entities.CoreSchema
{
    [Table("BusinessUnit", Schema = "CoreSchema")]
    public class BusinessUnit : AuditableEntity
    {
        public int BusinessUnitId { get; set; }
        public string? Name { get; set; }
        public int PersonId { get; set; }
        public bool IsMain { get; set; }
        public BusinessUnitStatus Status { get; set; }
        public int CompanyId { get; set; }
    }
}
