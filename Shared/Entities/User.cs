using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Shared.ValueObjects;

namespace Shared.Entities;

[Table("users", Schema = "code_first")]
public class User
{
    [Column("id"), Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Column("username"), Required, MaxLength(64)]
    public string Username { get; set; } = null!;
    
    [Column("email"), MaxLength(128)]
    public string? Email { get; set; }
    
    [Column("role")]
    public UserRole Role { get; set; } = UserRole.User;
    
    
    // Owned tables
    public AuditInfo AuditInfo { get; set; } = new();
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
    User,
    Admin,
    SuperAdmin
}