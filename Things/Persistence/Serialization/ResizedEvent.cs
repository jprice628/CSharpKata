using Newtonsoft.Json.Linq;

namespace Things.Persistence;

internal static partial class Serialization
{
    private static string Serialize(ResizedEvent e) => new JObject
    {
        ["Type"] = nameof(ResizedEvent),
        ["Timestamp"] = e.Timestamp.Value,
        ["NewSize"] = e.NewSize.Value
    }.ToString();

    private static Fin<ThingEvent> ToResizedEvent(JObject obj) =>
        from rawTimestamp in obj.GetRequiredValue<DateTimeOffset>("Timestamp")
        let timestamp = new Timestamp(rawTimestamp)
        from rawNewSize in obj.GetRequiredValue<int>("NewSize")
        from newSize in Size.New(rawNewSize)
        select (ThingEvent)new ResizedEvent(timestamp, newSize);
}