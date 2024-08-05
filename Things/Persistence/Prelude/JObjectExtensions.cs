using Newtonsoft.Json.Linq;

namespace Things.Persistence;

internal static class JObjectExtensions
{
    internal static Either<Error, TValue> GetRequiredValue<TValue>(this JObject obj, string key) =>
        from token in GetToken(obj, key)
        from nullable in TryGetValue<TValue>(token)
        from value in EnsureNonNullValue(nullable)
        select value;

    private static Either<Error, JToken> GetToken(JObject obj, string key) =>
        !obj.TryGetValue(key, out var token) ? Error.New($"JObject does not have the specified key: {key}.")
        : token is null ? Error.New("The token returned by the JObject is null.")
        : token;

    private static Either<Error, TValue?> TryGetValue<TValue>(JToken token) =>
        Try(() => token.Value<TValue>())
        .ToEither(ex => Error.New("An error occurred while getting the value of a JToken: " + ex.Message));

    private static Either<Error, TValue> EnsureNonNullValue<TValue>(TValue? value) =>
        value is null ? Error.New("The required value obtained from a JToken is null.")
        : value;
}
