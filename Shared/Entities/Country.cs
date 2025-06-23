using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Entities;

[Table("countries", Schema = "code_first")]
public class Country
{
    [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("name"), Required, StringLength(64)]
    public string Name { get; set; } = null!;
    
    
    // Nav props
    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    
    // Readonly props
    public int TeamCount => Teams.Count;
    public int PlayerCount => Players.Count;
}