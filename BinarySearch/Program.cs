﻿using LanguageExt;
using LanguageExt.Common;
using static BinarySearch.Functions;
using static LanguageExt.Prelude;

var arr = new int[] { 6, 9, 22, 24, 46, 52, 65, 76, 82, 96 };

Run(from input in GetInput(args)
    let result = Search(arr, input)
    select ResultMessage(input, result));

// "Runs" the program and prints the results
Unit Run(Fin<string> fin) => fin.Match(
    Succ: Print,
    Fail: PrintError);

// Attempts to read and parse the user's input
Fin<int> GetInput(string[] args) =>
    args.Length != 1 ? Error.New("Incorrect number of arguments provided.")
    : string.IsNullOrWhiteSpace(args[0]) ? Error.New("Argument is null or white space.")
    : !int.TryParse(args[0], out var result) ? Error.New("Unable to parse argument value")
    : result;

Unit Print(string text)
{
    Console.WriteLine(text);
    return unit;
}

string ResultMessage(int input, Option<int> mIndex) => mIndex.Match(
    Some: index => $"Index of {input} found at {index}.",
    None: () => $"Index for {input} not found.");

// Prints an error message and the program's usage instructions
Unit PrintError(Error err)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Error: " + err.Message);
    Console.ResetColor();
    Console.WriteLine("Usage: BinarySearch.exe <integer>");
    Console.WriteLine("       bs.bat <integer>");
    return unit;
}