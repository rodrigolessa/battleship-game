using System.Diagnostics.CodeAnalysis;
using BattleshipGame.Infrastructure.ReadModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleshipGame.Infrastructure.ReadModel.Mapping;

[ExcludeFromCodeCoverage]
public class GameBoardProjectionMapping : IEntityTypeConfiguration<GameBoardProjection>
{
    public void Configure(EntityTypeBuilder<GameBoardProjection> builder)
    {
        builder.ToTable("GameBoardProjection");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(RelationalModelType.IdMaxLength);
    }
}