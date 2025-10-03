using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BattleshipGame.Infrastructure.ReadModel.Mapping;

public class RelationalModelType
{
    public const int IdMaxLength = 36;
    public const int OriginIdMaxLength = 50;
    public const string EnumType = "tinyint";
    public const string Tiny = "tinyint";
    public const string Int = "int";
    public const string Big = "bigint";
    public const string Decimal = "decimal(19,10)";
    public const string Percentage = "decimal(11,10)";
    public const string DateTime = "datetime2";
    public const string Date = "date";
    public const string Boolean = "bit";

    public static ValueConverter<DateOnly, DateTime> DateOnlyConverter => new(
        d => d.ToDateTime(TimeOnly.MinValue),
        d => DateOnly.FromDateTime(d));
}