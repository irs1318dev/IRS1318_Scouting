using System;

namespace Scouter.Models
{
    public interface IAuditInfo
    {
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}
