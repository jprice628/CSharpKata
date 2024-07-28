using LanguageExt;
using static LanguageExt.Prelude;

namespace BinarySearch;

internal static class Functions
{
    // Uses a binary search algorith to find a value in an array and return its index
    // Note: the array is assumed to be in order.
    internal static Option<int> Search(int[] arr, int value) =>
        arr.Length == 0 ? None : Search(arr, value, 0, arr.Length - 1);

    private static Option<int> Search(int[] arr, int value, int lo, int hi) =>
        lo > hi ? None
        : Mid(lo, hi) is int mi && arr[mi] == value ? mi
        : value < arr[mi] ? Search(arr, value, lo, mi - 1)  // Discard right values
        : Search(arr, value, mi + 1, hi);                   // Discard left values

    // An extended version of the binary search that logs each recursion
    // Note: the array is assumed to be in order.
    internal static Writer<Logger, string, Option<int>> SearchW(int[] arr, int value) =>
        arr.Length == 0 
        ? () => (None, "Empty set.\n", false)
        : SearchW(arr, value, 0, arr.Length -1);

    private static Writer<Logger, string, Option<int>> SearchW(int[] arr, int value, int lo, int hi) =>
        from _ in tell<Logger, string>($"Search(lo: {lo}, hi: {hi})\n")
        from result in
            lo > hi ? Return(None)
            : Mid(lo, hi) is int mi && arr[mi] == value ? Return(mi)
            : value < arr[mi] ? SearchW(arr, value, lo, mi - 1)     // Discard right values
            : SearchW(arr, value, mi + 1, hi)                       // Discard left values
        select result;

    private static Writer<Logger, string, Option<int>> Return(Option<int> value) =>
        () => (value, string.Empty, false);

    private static int Mid(int lo, int hi) => 
        (hi - lo) / 2 + lo;
}
