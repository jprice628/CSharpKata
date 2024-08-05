using Newtonsoft.Json.Linq;

namespace Things.Persistence;

internal static partial class Serialization
{
    private static string Serialize(CreatedEvent e) => new JObject
    {
        { "Type", nameof(CreatedEvent) },
        { "Timestamp", e.Timestamp.Value },
        { "ThingId", e.ThingId.Value },
        { "Shape", e.Shape.ToString() },
        { "Size", e.Size.Value }
    }.ToString();

    private static Either<Error, ThingEvent> ToCreatedEvent(JObject obj) =>
        from rawTimestamp in obj.GetRequiredValue<DateTimeOffset>("Timestamp")
        let timestamp = new Timestamp(rawTimestamp)
        from rawThingId in obj.GetRequiredValue<Guid>("ThingId")
        from thingId in ThingId.New(rawThingId)
        from rawShape in obj.GetRequiredValue<string>("Shape")
        from shape in ToShape(rawShape)
        from rawSize in obj.GetRequiredValue<int>("Size")
        from size in Size.New(rawSize)
        select (ThingEvent)new CreatedEvent(timestamp, thingId, shape, size);
}
