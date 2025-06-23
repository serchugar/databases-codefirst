using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Entities.Joins;

namespace Postgres.Data.Configurations.Joins;

public class TeamTournamentConfiguration : IEntityTypeConfiguration<TeamTournament>
{
    public void Configure(EntityTypeBuilder<TeamTournament> builder)
    {
        // Relationship to Team
        builder.HasOne(tt => tt.Team)
            .WithMany(t => t.TeamTournaments)
            .HasForeignKey(tt => tt.TeamId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Relationship to Tournament
        builder.HasOne(tt => tt.Tournament)
            .WithMany(t => t.TeamTournaments)
            .HasForeignKey(tt => tt.TournamentId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.OwnsOne(tt => tt.AuditInfo, auditInfo =>
        {
            auditInfo.Property(ai => ai.CreatedAt);
            auditInfo.Property(ai => ai.UpdatedAt);
        });
    }
}