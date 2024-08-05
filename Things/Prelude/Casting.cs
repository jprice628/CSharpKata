namespace Things;

public static partial class Prelude
{
    /// <summary>
    /// Attempts to cast an object as a particular type
    /// </summary>
    /// <typeparam name="TResult">The target type</typeparam>
    /// <param name="obj">The object</param>
    /// <param name="errorMessage">An optional error message</param>
    /// <returns>An object of the specified type or an error</returns>
    public static Either<Error, TResult> As<TResult>(this object obj, string? errorMessage = null) =>
        obj is not TResult result ? Error.New(errorMessage ?? $"Unable to cast {obj.GetType().Name} to {typeof(TResult).Name}.")
        : result;
}
