using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Entities;

namespace Postgres.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.Username).IsUnique();
        
        builder.OwnsOne(u => u.AuditInfo, auditInfo =>
        {
            auditInfo.Property(ai => ai.CreatedAt);
            auditInfo.Property(ai => ai.UpdatedAt);
        });
        
        // UserRole enum is stored as a string in the database
        builder.Property(u => u.Role).HasConversion<string>();
    }
}