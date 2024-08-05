namespace Things;

public static partial class Prelude
{
    /// <summary>
    /// Ensures that a GUID is not empty
    /// </summary>
    /// <param name="guid">A guid</param>
    /// <param name="errorMessage">An optional error message</param>
    /// <returns>A unit or an error</returns>
    public static Either<Error, Unit> ErrorIfEmpty(this Guid guid, string? errorMessage = null) =>
        guid == Guid.Empty ? Error.New(errorMessage ?? "The supplied GUID is empty.")
        : unit;

    /// <summary>
    /// Parses a GUID
    /// </summary>
    /// <param name="value">A string</param>
    /// <param name="errorMessage">An optional error message</param>
    /// <returns>A GUID or an error</returns>
    public static Either<Error, Guid> ParseGuid(string value, string? errorMessage = null) =>
        !Guid.TryParse(value, out Guid result) ? Error.New(errorMessage ?? "Unable to parse GUID.")
        : result;
}
