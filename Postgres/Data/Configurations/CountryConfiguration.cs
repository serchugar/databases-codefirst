using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Entities;

namespace Postgres.Data.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasIndex(c => c.Name).IsUnique();
        
        // (Country) 1:N (Player)
        builder.HasMany(c => c.Players)
            .WithOne(p => p.Country)
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // (Country) 1:N (Team)
        builder.HasMany(c => c.Teams)
            .WithOne(t => t.Country)
            .HasForeignKey(t => t.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(c => c.Players).AutoInclude();
        builder.Navigation(c => c.Teams).AutoInclude();
        
        builder.OwnsOne(c => c.AuditInfo, auditInfo =>
        {
            auditInfo.Property(ai => ai.CreatedAt);
            auditInfo.Property(ai => ai.UpdatedAt);
        });
    }
}