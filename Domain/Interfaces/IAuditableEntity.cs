using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSolution.Domain.Interfaces
{
    public interface IAuditableEntity
    {
        DateTime CreationDate { get; set; }
        DateTime UpdateDate { get; set; }
        DateTime? DeletionDate { get; set; }
        int? CreationUserId { get; set; }
        int? UpdateUserId { get; set; }
    }
}
