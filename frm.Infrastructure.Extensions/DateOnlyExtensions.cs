using System.Globalization;

namespace frm.Infrastructure.Extensions;

public static class DateOnlyExtensions
{
    public const string ISOShortDateFormat = "yyyy-MM-dd";

    /// <summary>
    /// Convert a date only property to short ISO 8601 string
    /// </summary>
    /// <param name="dateOnly">The date</param>
    /// <returns>Date formatted in short ISO 8601 (yyyy-MM-dd)</returns>
    public static string ToISOShortDateString(this DateOnly dateOnly)
    {
        return dateOnly.ToString(ISOShortDateFormat, CultureInfo.InvariantCulture);
    }

    public static DateOnly FromISOShortDateStringToDateOnly(this string dateOnlyString)
    {
        return DateOnly.ParseExact(dateOnlyString, ISOShortDateFormat, CultureInfo.InvariantCulture);
    }

    public static bool TryFromISOShortDateStringToDateOnly(this string dateOnlyString, out DateOnly dateOnly)
    {
        if (string.IsNullOrEmpty(dateOnlyString))
        {
            dateOnly = default;
            return false;
        }

        return DateOnly.TryParseExact(dateOnlyString, ISOShortDateFormat, CultureInfo.InvariantCulture, 0, out dateOnly);
    }
}