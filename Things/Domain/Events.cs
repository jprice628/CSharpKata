namespace Domain;

/// <summary>
/// The base type for events related to things
/// </summary>
/// <param name="Timestamp">The time at which the event occurred</param>
public abstract record ThingEvent(Timestamp Timestamp);

/// <summary>
/// An event describing the creation of a thing
/// </summary>
/// <param name="Timestamp">The time at which the event occurred</param>
/// <param name="ThingId">The thing's ID</param>
/// <param name="Shape">The thing's initial shape</param>
/// <param name="Size">The thing's initial size</param>
public sealed record CreatedEvent(Timestamp Timestamp, ThingId ThingId, Shape Shape, Size Size)
    : ThingEvent(Timestamp);

/// <summary>
/// An event describing a change to a thing's shape
/// </summary>
/// <param name="Timestamp">The time at which the event occurred</param>
/// <param name="NewShape">The thing's new shape value</param>
public sealed record ReshapedEvent(Timestamp Timestamp, Shape NewShape)
    : ThingEvent(Timestamp);

/// <summary>
/// An event describing a change to a thing's size
/// </summary>
/// <param name="Timestamp">The time at which the event occurred</param>
/// <param name="NewSize">The thing's new size value</param>
public sealed record ResizedEvent(Timestamp Timestamp, Size NewSize)
    : ThingEvent(Timestamp);
