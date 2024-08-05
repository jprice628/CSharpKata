using Newtonsoft.Json.Linq;

namespace Things.Persistence;

internal static partial class Serialization
{
    internal static IEnumerable<string> Serialize(IEnumerable<ThingEvent> events) =>
        events.Select(Serialize);

    internal static Either<Error, IEnumerable<ThingEvent>> Deserialize(IEnumerable<string> json) =>
        json.Select(Deserialize).Sequence();

    private static string Serialize(ThingEvent e) => e switch
    {
        CreatedEvent created => Serialize(created),
        ReshapedEvent reshaped => Serialize(reshaped),
        ResizedEvent resized => Serialize(resized),
        _ => throw new NotImplementedException("Unexpected event type.")
    };

    private static Either<Error, ThingEvent> Deserialize(string json) =>
        string.IsNullOrWhiteSpace(json) ? Error.New("Cannot deserialize null or white space strings.")
        : JObject.Parse(json) is not JObject obj ? Error.New("Unable to parse JSON.")
        : from type in obj.GetRequiredValue<string>("Type")
          from e in Deserialize(type, obj)
          select e;

    private static Either<Error, ThingEvent> Deserialize(string type, JObject obj) => type switch
    {
        nameof(CreatedEvent) => ToCreatedEvent(obj),
        nameof(ReshapedEvent) => ToReshapedEvent(obj),
        nameof(ResizedEvent) => ToResizedEvent(obj),
        _ => throw new NotImplementedException("Unexpected type.")
    };
}