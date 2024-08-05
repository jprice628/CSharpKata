using LanguageExt;
using LanguageExt.Common;

namespace Things.Domain;

/// <summary>
/// Provides an expressive way to represent the time at which an event occurred
/// </summary>
/// <remarks>
/// Timestamps are value types, so they benefit from being represented using 
/// records. They do not have an universal constraints, so a record struct is 
/// used. 
/// </remarks>
public readonly record struct Timestamp(DateTimeOffset Value) : IComparable<Timestamp>
{
    /// <summary>
    /// Provides a timestamp for the current time in UTC
    /// </summary>
    public static Timestamp UtcNow => 
        new Timestamp(DateTimeOffset.UtcNow);

    /// <summary>
    /// Constructs a timestamp from a string value
    /// </summary>
    /// <param name="valueAsString">A string representing a DateTimeOffset value</param>
    /// <returns>A timestamp or an error</returns>
    public static Fin<Timestamp> New(string valueAsString) =>
        !DateTimeOffset.TryParse(valueAsString, out var value) ? Error.New("Unable to parse timestamp.")
        : new Timestamp(value);

    /// <inheritdoc/>
    public int CompareTo(Timestamp other) =>
        Value.CompareTo(other.Value);

    /// <summary>
    /// Converts the timestamp to universal time
    /// </summary>
    /// <returns>A timestamp</returns>
    public Timestamp ToUniversalTime() =>
        this with { Value = Value.ToUniversalTime() };

    /// <summary>
    /// Converts a timestamp to a DateTimeOffset by returning its value property
    /// </summary>
    /// <param name="timestamp">A timestamp</param>
    public static implicit operator DateTimeOffset(Timestamp timestamp) => timestamp.Value;
}
