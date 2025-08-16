using BattleshipGame.Infrastructure.ReadModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleshipGame.Infrastructure.ReadModel.Mapping;

public class VesselProjectionMapping : IEntityTypeConfiguration<VesselProjection>
{
    public void Configure(EntityTypeBuilder<VesselProjection> builder)
    {
        builder.ToTable("GameBoardProjection");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(RelationalModelType.IdMaxLength);
    }
}