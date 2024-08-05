using Newtonsoft.Json.Linq;

namespace Things.Persistence;

internal static class JObjectExtensions
{
    internal static Either<Error, TValue> GetRequiredValue<TValue>(this JObject obj, string key) =>
        !obj.TryGetValue(key, out var token) ? Error.New($"JObject does not have the specified key: {key}.")
        : token is null ? Error.New("The token returned by the JObject is null.")
        : from nullable in GetValue<TValue>(token)
          from value in EnsureNonNull(nullable)
          select value;

    private static Either<Error, TValue?> GetValue<TValue>(JToken token) =>
        Try(() => token.Value<TValue>())
        .ToEither(ex => Error.New("An error occurred while getting the value of a JToken: " + ex.Message));

    private static Either<Error, TValue> EnsureNonNull<TValue>(TValue? value) =>
        value is null ? Error.New("The required value obtained from a JToken is null.")
        : value;
}
