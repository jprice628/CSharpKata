namespace Things;

public static partial class Prelude
{
    /// <summary>
    /// Parses a DateTimeOffset value
    /// </summary>
    /// <param name="value">The string to parse</param>
    /// <param name="errorMessage">An optional error message</param>
    /// <returns>A DateTimeOffset or error</returns>
    public static Either<Error, DateTimeOffset> ParseDateTimeOffset(string value, string? errorMessage = null) =>
        !DateTimeOffset.TryParse(value, out var result) ? Error.New(errorMessage ?? "Unable to parse timestamp.")
        : result;
}
