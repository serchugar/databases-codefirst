using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.ValueObjects;

// Class intended to be used as an owned/extended table. The owned policy is defined in the modelBuilder
public class AuditInfo
{
    [Column("created_at")] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // This value needs to be assigned manually in each update
}