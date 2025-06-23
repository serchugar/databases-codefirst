using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Entities.Joins;
using Shared.ValueObjects;

namespace Shared.Entities;

[Table("tournaments", Schema = "code_first")]
public class Tournament
{
    [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column("name"), Required, StringLength(128)]
    public string Name { get; set; } = null!;
    
    // Owned tables
    public AuditInfo AuditInfo { get; set; } = new();
    
    // Nav props
    public virtual ICollection<TeamTournament> TeamTournaments { get; set; } = new List<TeamTournament>();
    
    // Readonly props
    public int TeamsCount => TeamTournaments.Count;
}