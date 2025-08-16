using BattleshipGame.Infrastructure.ReadModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleshipGame.Infrastructure.ReadModel.Mapping;

public class PanelProjectionMapping : IEntityTypeConfiguration<PanelProjection>
{
    public void Configure(EntityTypeBuilder<PanelProjection> builder)
    {
        builder.ToTable("GameBoardProjection");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(RelationalModelType.IdMaxLength);
    }
}