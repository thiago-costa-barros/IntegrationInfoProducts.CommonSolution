using CommonSolution.Entities.Common;
using CommonSolution.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.CoreSchema
{
    [Table("Person", Schema = "CoreSchema")]
    public class Person: AuditableEntity
    {
        public int PersonId { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? TaxNumber { get; set; }
        public PersonType Type { get; set; }
    }
}
