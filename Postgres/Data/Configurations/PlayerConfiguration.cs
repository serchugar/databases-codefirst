﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Entities;

namespace Postgres.Data.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.Navigation(p => p.Team).AutoInclude();
        builder.Navigation(p => p.Country).AutoInclude();
        
        builder.OwnsOne(c => c.AuditInfo, auditInfo =>
        {
            auditInfo.Property(ai => ai.CreatedAt);
            auditInfo.Property(ai => ai.UpdatedAt);
        });
    }
}