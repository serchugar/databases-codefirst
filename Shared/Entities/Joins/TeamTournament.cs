using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.ValueObjects;

namespace Shared.Entities.Joins;

[Table("team_tournaments", Schema = "code_first")]
public class TeamTournament
{
    [Column("team_id"), Key]
    public int TeamId { get; set; }
    
    [Column("tournament_id"), Key]
    public int TournamentId { get; set; }

    // Owned tables
    public AuditInfo AuditInfo { get; set; } = new();
    
    // Nav props
    public Team Team { get; set; } = null!;
    public Tournament Tournament { get; set; } = null!;
}