using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace CSharpKata;

/// <summary>
/// Provides application-level functions
/// </summary>
internal static class Application
{
    /// <summary>
    /// "Runs" the program and prints the results
    /// </summary>
    /// <param name="fin">The "program" to be run</param>
    /// <returns>A unit</returns>
    internal static Unit Run(Fin<string> fin) => fin.Match(
        Succ: Print,
        Fail: PrintError);

    /// <summary>
    /// Attempts to read and parse the user's input
    /// </summary>
    /// <param name="args">The command line arguments</param>
    /// <returns>The user's input (a value to search for) or an error</returns>
    internal static Fin<int> GetInput(string[] args) =>
        args.Length != 1 ? Error.New("Incorrect number of arguments provided.")
        : string.IsNullOrWhiteSpace(args[0]) ? Error.New("Argument is null or white space.")
        : !int.TryParse(args[0], out var result) ? Error.New("Unable to parse argument value")
        : result;

    /// <summary>
    /// Wraps Console.WriteLine and returns a Unit to make it work better with 
    /// functional-style code
    /// </summary>
    /// <param name="text">The text to print</param>
    /// <returns>A unit</returns>
    internal static Unit Print(string text)
    {
        Console.WriteLine(text);
        return unit;
    }

    /// <summary>
    /// Prints result titles in green to make them easier to see
    /// </summary>
    /// <param name="text">The title text to print</param>
    /// <returns>A unit</returns>
    internal static Unit PrintTitle(string text)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(text);
        Console.ResetColor();
        return unit;
    }

    /// <summary>
    /// Formats a result message depending on whether the binary search returned an index
    /// </summary>
    /// <param name="input">The user's input</param>
    /// <param name="mIndex">The index found by the search</param>
    /// <returns>A result message</returns>
    internal static string ResultMessage(int input, Option<int> mIndex) => mIndex.Match(
        Some: index => $"Index of {input} found at {index}.",
        None: () => $"Index for {input} not found.");

    /// <summary>
    /// Formats a result message and appends it to the log returned by the SearchW function
    /// </summary>
    /// <param name="input">The user's input</param>
    /// <param name="mIndex">The index found by the search</param>
    /// <param name="log">The log recorded by the search</param>
    /// <returns></returns>
    internal static string ResultMessageW(int input, Option<int> mIndex, string log) =>
        log + ResultMessage(input, mIndex);

    /// <summary>
    /// Prints an error message and the program's usage instructions
    /// </summary>
    /// <param name="err">The err to print</param>
    /// <returns>A unit</returns>
    private static Unit PrintError(Error err)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error: " + err.Message);
        Console.ResetColor();
        Console.WriteLine("Usage: BinarySearch.exe <integer>");
        Console.WriteLine("       bs.bat <integer>");
        return unit;
    }
}
