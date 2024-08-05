using System.Text.RegularExpressions;

namespace Things.Persistence;

public sealed partial record PartialThingId
{
    public string Value { get; private init; }

    private PartialThingId(string value) =>
        Value = value;

    public static Fin<PartialThingId> New(string value) =>
        string.IsNullOrWhiteSpace(value) ? Error.New("PartialThingId: value cannot be null or white space.")
        : !Pattern().IsMatch(value) ? Error.New("PartialThingId: value does not match the expected pattern.")
        : new PartialThingId(value);

    public static PartialThingId New(ThingId thingId) =>
        new(thingId.Value.ToString()[..8]);

    public static implicit operator string(PartialThingId id) =>
        id.Value;

    [GeneratedRegex(@"^[A-Za-z0-9]{8}$", RegexOptions.Compiled)]
    private static partial Regex Pattern();
}
