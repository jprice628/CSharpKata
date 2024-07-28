using LanguageExt;
using static LanguageExt.Prelude;

namespace BinarySearch;

internal static class Functions
{
    // Uses a binary search algorith to find a value in an array and return its index.
    // Note: the array is assumed to be in order.
    internal static Option<int> Search(int[] arr, int value) =>
        arr.Length == 0 ? None : Search(arr, value, 0, arr.Length - 1);

    private static Option<int> Search(int[] arr, int value, int lo, int hi) =>
        lo > hi ? None
        : Mid(lo, hi) is int mi && arr[mi] == value ? mi
        : value < arr[mi] ? Search(arr, value, lo, mi - 1)  // Discard right values
        : Search(arr, value, mi + 1, hi);                   // Discard left values

    private static int Mid(int lo, int hi) => 
        (hi - lo) / 2 + lo;
}
