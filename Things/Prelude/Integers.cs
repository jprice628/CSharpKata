namespace Things;

public static partial class Prelude
{
    /// <summary>
    /// Ensures that an integer value is within a specified domain
    /// </summary>
    /// <param name="value">The integer value to check</param>
    /// <param name="min">The inclusive lower bounds</param>
    /// <param name="max">The inclusive upper bounds</param>
    /// <returns>The value if it passes the check; otherwise an error</returns>
    public static Either<Error, Unit> ErrorIfNotBetween(this int value, int min, int max, string? errorMessage = null) =>
        value < min || value > max ? Error.New(errorMessage ?? $"The specified string must be between {min} and {max}.")
        : unit;        
}
