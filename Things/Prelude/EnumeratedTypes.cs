namespace Things;

public static partial class Prelude
{
    /// <summary>
    /// Parses an enumerated type from a string.
    /// </summary>
    /// <typeparam name="TEnum">The type to return</typeparam>
    /// <param name="value">The string value to be parsed</param>
    /// <returns>A value of the specified type or an error</returns>
    public static Either<Error, TEnum> ParseEnum<TEnum>(string value)
        where TEnum : struct =>
        Enum.TryParse<TEnum>(value, true, out var result)
        ? result
        : Error.New($"Unable to parse {typeof(TEnum).Name} from value '{value}'.");
}
