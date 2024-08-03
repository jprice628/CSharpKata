using Newtonsoft.Json.Linq;

namespace Things.Persistence;

internal static partial class Serialization
{
    private static string Serialize(ReshapedEvent e) => new JObject
    {
        ["Type"] = nameof(ReshapedEvent),
        ["Timestamp"] = e.Timestamp.Value,
        ["NewShape"] = e.NewShape.ToString()
    }.ToString();

    private static Fin<ThingEvent> ToReshapedEvent(JObject obj) =>
        from rawTimestamp in obj.GetRequiredValue<DateTimeOffset>("Timestamp")
        let timestamp = new Timestamp(rawTimestamp)
        from rawNewShape in obj.GetRequiredValue<string>("NewShape")
        from newShape in ToShape(rawNewShape)
        select (ThingEvent)new ReshapedEvent(timestamp, newShape);
}