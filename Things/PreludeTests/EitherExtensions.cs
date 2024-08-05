namespace Things.Testing;

internal static class EitherExtensions
{
    internal static T Value<T>(this Either<Error, T> fin) => fin.Match(
        Right: t => t,
        Left: err => throw new InvalidOperationException("Expected value but encountered error: " + err.Message));

    internal static Error Error<T>(this Either<Error, T> fin) => fin.Match(
        Right: _ => throw new InvalidOperationException("Expected an error, but encountered a value."),
        Left: err => err);
}
