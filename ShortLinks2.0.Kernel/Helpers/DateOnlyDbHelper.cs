using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ShortLinks.Kernel.Helpers;
/// <summary>
/// Converts <see cref="DateOnly" /> to <see cref="DateTime"/> and vice versa.
/// </summary>
public class DateOnlyDbConverter : ValueConverter<DateOnly, DateTime>
{
    /// <summary>
    /// Creates a new instance of this converter.
    /// </summary>
    public DateOnlyDbConverter()
        : base(d => d.ToDateTime(TimeOnly.MinValue),
            d => DateOnly.FromDateTime(d))
    { }
}

/// <summary>
/// Compares <see cref="DateOnly" />.
/// </summary>
public class DateOnlyDbComparer : ValueComparer<DateOnly>
{
    /// <summary>
    /// Creates a new instance of this converter.
    /// </summary>
    public DateOnlyDbComparer()
        : base((d1, d2) => d1 == d2 && d1.DayNumber == d2.DayNumber,
        d => d.GetHashCode())
    { }
}
