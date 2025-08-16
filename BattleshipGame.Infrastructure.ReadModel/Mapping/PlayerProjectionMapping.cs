using BattleshipGame.Infrastructure.ReadModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleshipGame.Infrastructure.ReadModel.Mapping;

public class PlayerProjectionMapping : IEntityTypeConfiguration<PlayerProjection>
{
    public void Configure(EntityTypeBuilder<PlayerProjection> builder)
    {
        builder.ToTable("GameBoardProjection");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(RelationalModelType.IdMaxLength);
    }
}