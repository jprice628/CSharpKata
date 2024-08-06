using System.Collections.Immutable;

namespace Things.Domain;

/// <summary>
/// An entity which properties and stuff
/// </summary>
public sealed record Thing
{
    /// <summary>
    /// Uniquely identifies the thing
    /// </summary>
    public ThingId Id { get; private init; }

    /// <summary>
    /// Describes the thing's shape, i.e., round, curvy, angular
    /// </summary>
    public Shape Shape { get; private init; }

    /// <summary>
    /// Describes the thing's size
    /// </summary>
    public Size Size { get; private init; }

    /// <summary>
    /// Tracks the changes that have been made to the thing
    /// </summary>
    public ImmutableList<ThingEvent> Changes { get; private init; }

    /// <summary>
    /// Constructs a new thing
    /// </summary>
    /// <param name="id">Uniquely identifies the thing</param>
    /// <param name="shape">Describes the thing's shape, i.e., round, curvy, angular</param>
    /// <param name="size">Describes the thing's size</param>
    private Thing(CreatedEvent e, bool applying) =>
        (Id, Shape, Size, Changes) = (e.ThingId, e.Shape, e.Size, applying ? [e] : []);

    /// <summary>
    /// Constructs a new thing
    /// </summary>
    /// <param name="shape">Describes the thing's shape, i.e., round, curvy, angular</param>
    /// <param name="size">Describes the thing's size</param>
    /// <returns>A thing</returns>
    public static Thing New(Shape shape, Size size) =>
        new(new(Timestamp.UtcNow, ThingId.New(), shape, size), true);

    /// <summary>
    /// Constructs a new thing from a stream of events
    /// </summary>
    /// <param name="events">A set of events, starting with a created event</param>
    /// <returns>A thing or an error</returns>
    public static Either<Error, Thing> New(IEnumerable<ThingEvent> events) =>
        from _ in ErrorIfEmpty(events, "A thing cannot be created from an empty stream.")
        let ordered = events.OrderBy(e => e.Timestamp)
        from created in GetCreatedEvent(ordered)
        let tail = ordered.Skip(1)
        select tail.Aggregate(new Thing(created, false), (thing, e) => thing.When(e, false));

    /// <summary>
    /// Gets the first event from a collection and casts it as a CreatedEvent
    /// </summary>
    /// <param name="events">A collection of events</param>
    /// <returns>A created event or an error</returns>
    private static Either<Error, CreatedEvent> GetCreatedEvent(IEnumerable<ThingEvent> events) =>
        from firstEvent in events.FirstItem()
        from created in firstEvent.As<CreatedEvent>("The first event in a thing's stream must be a created event.")
        select created;

    /// <summary>
    /// Changes the thing's shape
    /// </summary>
    /// <param name="newShape">The new shape value</param>
    /// <returns>A thing</returns>
    public Thing Reshape(Shape newShape) =>
        Shape == newShape ? this
        : When(new ReshapedEvent(Timestamp.UtcNow, newShape), true);

    /// <summary>
    /// Changes a thing's size
    /// </summary>
    /// <param name="newSize">The new size value</param>
    /// <returns>A thing</returns>
    public Thing Resize(Size newSize) =>
        Size == newSize ? this
        : When(new ResizedEvent(Timestamp.UtcNow, newSize), true);

    /// <summary>
    /// Updates a thing based on an event
    /// </summary>
    /// <param name="e">An event</param>
    /// <param name="applying">
    /// Indicates whether the event is being applied. This will be true when the 
    /// Reshape method has been called and false when constructing a thing from a 
    /// stream of events.
    /// </param>
    /// <returns>A thing</returns>
    private Thing When(ThingEvent e, bool applying) => e switch
    {
        ReshapedEvent reshaped => When(reshaped, applying),
        ResizedEvent resized => When(resized, applying),
        _ => this
    };

    /// <summary>
    /// Updates the thing based on a reshaped event
    /// </summary>
    /// <param name="e">The reshaped event</param>
    /// <param name="applying">
    /// Indicates whether the event is being applied. This will be true when the 
    /// Reshape method has been called and false when constructing a thing from a 
    /// stream of events.
    /// </param>
    /// <returns>A new thing with its properties set according to the event</returns>
    private Thing When(ReshapedEvent e, bool applying) => this with 
    { 
        Shape = e.NewShape,
        Changes = applying ? Changes.Add(e) : Changes
    };

    /// <summary>
    /// Updates the thing based on a resized event
    /// </summary>
    /// <param name="e">The resized event</param>
    /// <param name="applying">
    /// Indicates whether the event is being applied. This will be true when the 
    /// Resize method has been called and false when constructing a thing from a 
    /// stream of events.
    /// </param>
    /// <returns>A new thing with its properties set according to the event</returns>
    private Thing When(ResizedEvent e, bool applying) => this with
    {
        Size = e.NewSize,
        Changes = applying ? Changes.Add(e) : Changes
    };
}
