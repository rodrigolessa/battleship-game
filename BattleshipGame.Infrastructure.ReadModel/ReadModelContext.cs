using System.Diagnostics.CodeAnalysis;
using BattleshipGame.Infrastructure.ReadModel.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BattleshipGame.Infrastructure.ReadModel;

[ExcludeFromCodeCoverage]
public class ReadModelContext : DbContext
{
    public ReadModelContext(DbContextOptions<ReadModelContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new GameProjectionMapping())
            .ApplyConfiguration(new PlayerProjectionMapping())
            .ApplyConfiguration(new GameBoardProjectionMapping())
            .ApplyConfiguration(new PanelProjectionMapping())
            .ApplyConfiguration(new VesselProjectionMapping());
    }
}