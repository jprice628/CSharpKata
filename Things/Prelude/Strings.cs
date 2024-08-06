using System.Text.RegularExpressions;

namespace Things;

public static partial class Prelude
{
    /// <summary>
    /// Ensures that a string is not null or white space
    /// </summary>
    /// <param name="value">A string value</param>
    /// <param name="errorMessage">An optional error message</param>
    /// <returns>A unit or an error</returns>
    public static Either<Error, Unit> ErrorIfNullOrWhiteSpace(this string value, string? errorMessage = null) =>
        string.IsNullOrWhiteSpace(value) ? Error.New(errorMessage ?? "String is null or white space.")
        : unit;

    /// <summary>
    /// Ensures that a string matchs a regular expression
    /// </summary>
    /// <param name="value">A string value</param>
    /// <param name="regex">A regular expression</param>
    /// <param name="errorMessage">An optional error message</param>
    /// <returns>A unit or an error</returns>
    public static Either<Error, Unit> ErrorIfDoesNotMatch(this string value, Regex regex, string? errorMessage = null) =>
        !regex.IsMatch(value) ? Error.New(errorMessage ?? "String does not match regular expression.")
        : unit;
}
