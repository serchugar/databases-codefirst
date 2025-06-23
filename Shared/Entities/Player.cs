using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Entities;

[Table("players", Schema = "code_first")]
public class Player
{
    [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("name"), Required, StringLength(64)]
    public string Name { get; set; } = null!;
    
    [Column("team_id"), Required]
    public int TeamId { get; set; }
    
    [Column("country_id"), Required]
    public int CountryId { get; set; }
    
    
    // Nav props
    public virtual Team Team { get; set; } = null!;
    public virtual Country Country { get; set; } = null!;
}