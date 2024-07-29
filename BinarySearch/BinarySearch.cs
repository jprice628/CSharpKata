using LanguageExt;
using static LanguageExt.Prelude;

namespace CSharpKata;

/// <summary>
/// Provides the binary search functions Search and SearchW
/// </summary>
internal static class BinarySearch
{
    /// <summary>
    /// Uses a binary search algorithm to find a value in an array and return its index
    /// </summary>
    /// <param name="arr">An array of integer values to search</param>
    /// <param name="value">The value to find in the array</param>
    /// <returns>An optional integer value</returns>
    /// <remarks> The array is assumed to be in order.</remarks>
    internal static Option<int> Search(int[] arr, int value) =>
        arr.Length == 0 ? None : Search(arr, value, 0, arr.Length - 1);

    /// <summary>
    /// Uses a binary search algorithm to find a value between two indexes in an array
    /// </summary>
    /// <param name="arr">An array of integer values to search</param>
    /// <param name="value">The value to find in the array</param>
    /// <param name="lo">The lowest index to be searched</param>
    /// <param name="hi">The highest index to be searched</param>
    /// <returns>An optional integer value</returns>
    /// <remarks>This form of the function uses recursion.</remarks>
    private static Option<int> Search(int[] arr, int value, int lo, int hi) =>
        lo > hi ? None
        : Mid(lo, hi) is int mi && arr[mi] == value ? mi
        : value < arr[mi] ? Search(arr, value, lo, mi - 1)  // Discard right values (including mid)
        : Search(arr, value, mi + 1, hi);                   // Discard left values (including mid)

    /// <summary>
    /// An extended version of the binary search that logs each recursion
    /// </summary>
    /// <param name="arr">An array of integer values to search</param>
    /// <param name="value">The value to find in the array</param>
    /// <returns>A Writer delegate</returns>
    /// <remarks> The array is assumed to be in order.</remarks>
    internal static Writer<Logger, string, Option<int>> SearchW(int[] arr, int value) =>
        arr.Length == 0 
        ? () => (None, "Empty set.\n", false)
        : SearchW(arr, value, 0, arr.Length -1);

    /// <summary>
    /// Uses an extended version of the binary search to find a value between 
    /// two indexes in an array and log each recursion
    /// </summary>
    /// <param name="arr">An array of integer values to search</param>
    /// <param name="value">The value to find in the array</param>
    /// <param name="lo">The lowest index to be searched</param>
    /// <param name="hi">The highest index to be searched</param>
    /// <returns>A Writer delegate</returns>
    /// <remarks>This form of the function uses recursion.</remarks>
    private static Writer<Logger, string, Option<int>> SearchW(int[] arr, int value, int lo, int hi) =>
        from _ in tell<Logger, string>($"Search(lo: {lo}, hi: {hi})\n")
        from result in
            lo > hi ? Return(None)
            : Mid(lo, hi) is int mid && arr[mid] == value ? Return(mid)
            : value < arr[mid] ? SearchW(arr, value, lo, mid - 1)     // Discard right values (including mid)
            : SearchW(arr, value, mid + 1, hi)                       // Discard left values (including mid)
        select result;

    /// <summary>
    /// Converts a return value into a Writer delegate
    /// </summary>
    /// <param name="value">A return value</param>
    /// <returns>A Writer delegate</returns>
    private static Writer<Logger, string, Option<int>> Return(Option<int> value) =>
        () => (value, string.Empty, false);

    /// <summary>
    /// Calculates the midpoint between two indexes
    /// </summary>
    /// <param name="lo">The lower index</param>
    /// <param name="hi">The higher index</param>
    /// <returns>A midpoint index</returns>
    private static int Mid(int lo, int hi) => 
        (hi - lo) / 2 + lo;
}
