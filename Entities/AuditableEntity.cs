using CommonSolution.Interfaces;

namespace CommonSolution.Entities
{
    public class AuditableEntity : IAuditableEntity
    {
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeletionDate { get; set; }
        public int? CreationUserId { get; set; }
        public int? UpdateUserId { get; set; }
    }
}
