using CommonSolution.Entities.Common;
using CommonSolution.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.CoreSchema
{
    public class BusinessUnit
    {
        [Table("BusinessUnit", Schema = "CoreSchema")]
        public class CompanyBranch : AuditableEntity
        {
            public int BusinessUnitId { get; set; }
            public string? Name { get; set; }
            public int PersonId { get; set; }
            public bool IsMain { get; set; }
            public BusinessUnitStatus Status { get; set; }
            public int CompanyId { get; set; }  
        }
    }
}
