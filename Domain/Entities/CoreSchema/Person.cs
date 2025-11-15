using CommonSolution.Domain.Entities.Common;
using CommonSolution.Domain.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Domain.Entities.CoreSchema
{
    [Table("Person", Schema = "CoreSchema")]
    public class Person: AuditableEntity
    {
        public int PersonId { get; set; }
        public string? Name { get; set; }
        public string? TaxNumber { get; set; }
        public PersonType Type { get; set; }
        public PersonStatus Status { get; set; }
    }
}
