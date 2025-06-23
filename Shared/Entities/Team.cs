using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Entities;

[Table("teams", Schema = "code_first")]
public class Team
{
    [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("name"), Required, StringLength(64)]
    public string Name { get; set; } = null!;
    
    [Column("country_id"), Required]
    public int CountryId { get; set; }
    
    
    // Nav props
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    public virtual Country Country { get; set; } = null!;
    
    // Readonly props
    public int PlayerCount => Players.Count;
}