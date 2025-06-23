using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Entities;

namespace Postgres.Data.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        // This way teams with same name are allowed but in different countries
        // E.g: There is Barcelona in Spain and Barcelona in Venezuela
        builder.HasIndex(t => new {t.Name, t.CountryId}).IsUnique();
        
        // (Team) 1:N (Player)
        // Each Team can have many Players
        // Each Player can only be in one Team
        builder.HasMany(t => t.Players)
            .WithOne(p => p.Team)
            .HasForeignKey(p => p.TeamId)        // Player is dependent class (has the FK)
            .OnDelete(DeleteBehavior.Restrict);  // OnDelete applies to dependent class
            // Cannot delete a Team unless all of its Players have been deleted
        
        // AutoInclude Nav property 'Country' in the LINQ consults to an IQueryable
        builder.Navigation(t => t.Country).AutoInclude();
        
        // AutoInclude Nav property 'Players' in the LINQ consults to an IQueryable
        builder.Navigation(t => t.Players).AutoInclude();
        
        builder.OwnsOne(c => c.AuditInfo, auditInfo =>
        {
            auditInfo.Property(ai => ai.CreatedAt);
            auditInfo.Property(ai => ai.UpdatedAt);
        });
    }
}