namespace Things.Domain;

/// <summary>
/// Provides an expressive way to represent the ID of a thing.
/// </summary>
/// <remarks>
/// A ThingId is a value, so it benefits from being a record instead of a class.
/// A record class is being used instead of a record struct because the value has 
/// a universal constraint: it cannot be an empty GUID.
/// </remarks>
public sealed record ThingId
{
    /// <summary>
    /// The value of the ID
    /// </summary>
    public Guid Value { get; private init; }

    /// <summary>
    /// Constructs a new ThingId
    /// </summary>
    /// <param name="value">The value of the ID</param>
    /// <remarks>
    /// This constructor assumes that the value has been validated by one of 
    /// the factory methods below.
    /// </remarks>
    private ThingId(Guid value) =>
        Value = value;

    /// <summary>
    /// Constructs a new ThingId with a random GUID
    /// </summary>
    /// <returns>A ThingId</returns>
    public static ThingId New() =>
        new ThingId(Guid.NewGuid());

    /// <summary>
    /// Constructs a new ThingId
    /// </summary>
    /// <param name="value">The value of the ID</param>
    /// <returns>A ThingID or an error</returns>
    public static Either<Error, ThingId> New(Guid value) =>
        value == Guid.Empty ? Error.New("The value of a thing ID cannot be an empty GUID.")
        : new ThingId(value);

    /// <summary>
    /// Constructs a new ThingId
    /// </summary>
    /// <param name="guidAsString">The value of the ID as a string</param>
    /// <returns>A ThingID or an error</returns>
    public static Either<Error, ThingId> New(string guidAsString) =>
        !Guid.TryParse(guidAsString, out var guid) ? Error.New("Unable to parse thing ID.")
        : New(guid);


    /// <summary>
    /// Converts a ThingId to a Guid by returning its value
    /// </summary>
    /// <param name="thingId">A ThingId</param>
    public static implicit operator Guid(ThingId thingId) => thingId.Value;
}
