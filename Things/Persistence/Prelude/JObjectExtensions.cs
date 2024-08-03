using Newtonsoft.Json.Linq;

namespace Things.Persistence;

internal static class JObjectExtensions
{
    internal static Fin<TValue> GetRequiredValue<TValue>(this JObject obj, string key) =>
        !obj.TryGetValue(key, out var token) ? Error.New("JObject.GetRequiredValue: key not found.")
        : token is null ? Error.New("JObject.GetRequiredValue: token is null")
        : Try(() => token.Value<TValue>()).Try().Match<Fin<TValue>>(
            Succ: value => value is null ? Error.New("JObject.GetRequiredValue: required value is null") : value,
            Fail: ex => Error.New(ex));
}
