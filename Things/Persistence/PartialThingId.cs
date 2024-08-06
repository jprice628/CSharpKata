using System.Text.RegularExpressions;

namespace Things.Persistence;

/// <summary>
/// A shorter representation of a ThingId
/// </summary>
public sealed partial record PartialThingId
{
    /// <summary>
    /// The value of the partial ID
    /// </summary>
    public string Value { get; private init; }

    /// <summary>
    /// Constructs a new partial thing ID
    /// </summary>
    /// <param name="value">The value of the partial ID</param>
    private PartialThingId(string value) =>
        Value = value;

    /// <summary>
    /// Constructs a new partial thing ID from a thing ID
    /// </summary>
    /// <param name="thingId">A thing ID</param>
    /// <returns>A partial thing ID</returns>
    public static PartialThingId New(ThingId thingId) =>
        new(thingId.Value.ToString()[..8]);

    /// <summary>
    /// Constructs a partial thing ID from a string
    /// </summary>
    /// <param name="value">A string value</param>
    /// <returns>A partial thing ID or an error</returns>
    public static Either<Error, PartialThingId> New(string value) =>
        from _1 in value.ErrorIfNullOrWhiteSpace("A partial thing ID cannot be null or white space.")
        from _2 in value.ErrorIfDoesNotMatch(Pattern(), "The partial thing ID does not match the expected pattern.")
        select new PartialThingId(value);

    /// <summary>
    /// Converts a partial thing ID to a string by returning its value
    /// </summary>
    /// <param name="id"></param>
    public static implicit operator string(PartialThingId id) =>
        id.Value;

    /// <summary>
    /// Gets the expected pattern for partial thing IDs
    /// </summary>
    /// <returns>A regular expression</returns>
    [GeneratedRegex(@"^[A-Za-z0-9]{8}$", RegexOptions.Compiled)]
    private static partial Regex Pattern();
}
