using System.ComponentModel.DataAnnotations.Schema;
using Shared.ValueObjects;

namespace Shared.Entities.Joins;

[Table("team_tournaments", Schema = "code_first")]
// The composite key must be defined in the ModelBuilder
public class TeamTournament
{
    [Column("team_id")]
    public int TeamId { get; set; }
    
    [Column("tournament_id")]
    public int TournamentId { get; set; }

    // Owned tables
    public AuditInfo AuditInfo { get; set; } = new();
    
    // Nav props
    public Team Team { get; set; } = null!;
    public Tournament Tournament { get; set; } = null!;
}