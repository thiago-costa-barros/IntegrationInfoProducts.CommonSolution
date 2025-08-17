using CommonSolution.Entities.Common;
using CommonSolution.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.CoreSchema
{
    [Table("CompanyBranch", Schema = "CoreSchema")]
    public class CompanyBranch: AuditableEntity
    {
        public int CompanyBranchId { get; set; }
        public string? Name { get; set; }
        public string? TaxNumber { get; set; }
        public int CompanyId { get; set; }
        public bool IsMainBranch { get; set; }
        public CompanyBranchStatus Status { get; set; }
        public int PersonId { get; set; }
    }
}
