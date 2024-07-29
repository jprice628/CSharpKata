namespace Domain;

public abstract record ThingEvent(Timestamp Timestamp);

public sealed record CreatedEvent(Timestamp Timestamp, ThingId ThingId, Shape Shape, Size Size)
    : ThingEvent(Timestamp);

public sealed record ReshapedEvent(Timestamp Timestamp, Shape NewShape)
    : ThingEvent(Timestamp);

public sealed record ResizedEvent(Timestamp Timestamp, Size NewSize)
    : ThingEvent(Timestamp);
