using LanguageExt.TypeClasses;

namespace BinarySearch;

internal readonly record struct Logger : Monoid<string>
{
    public string Append(string x, string y) =>
        x + y;

    public string Empty() =>
        string.Empty;
}
