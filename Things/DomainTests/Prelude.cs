using LanguageExt;
using LanguageExt.Common;

namespace DomainTests;

internal static class Prelude
{
    internal static T Value<T>(this Fin<T> fin) => fin.Match(
        Succ: t => t,
        Fail: err => throw new InvalidOperationException("Expected value but encountered error: " + err.Message));

    internal static Error Error<T>(this Fin<T> fin) => fin.Match(
        Succ: _ => throw new InvalidOperationException("Expected an error, but encountered a value."),
        Fail: err => err);
}
