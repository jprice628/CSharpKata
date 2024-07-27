using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

Run(from input in GetInput(args)
    let _ = For1To(input, (_, n) => Print(FizzBuzz(n)))
    select unit);

// "Runs" the program and prints the results
Unit Run(Fin<Unit> fin) => fin.Match(
    Succ: _ => Print("Program complete!"),
    Fail: PrintError);

// Attempts to read and parse the user's input
Fin<int> GetInput(string[] args) =>
    args.Length != 1 ? Error.New("Incorrect number of arguments provided.")
    : string.IsNullOrWhiteSpace(args[0]) ? Error.New("Argument is null or white space.")
    : !int.TryParse(args[0], out var result) ? Error.New("Unable to parse argument value")
    : result < 1 ? Error.New("Argument is less than one.")
    : result;

// Maps an integer value to a FizzBuzz string
string FizzBuzz(int n) =>
    // There are more clever ways to do this, but this approach is simple and straigh-forward.
    (n % 3) + (n % 5) == 0 ? $"{n} FizzBuzz"
    : (n % 3) == 0 ? $"{n} Fizz"
    : (n % 5) == 0 ? $"{n} Buzz"
    : n.ToString();

// Wraps Console.WriteLine in a function that returns a Unit because void is messy
Unit Print(string text)
{
    Console.WriteLine(text);
    return unit;
}

// Prints an error message and the program's usage instructions
Unit PrintError(Error err)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Error: " + err.Message);
    Console.ResetColor();
    Console.WriteLine("Usage: FizzBuzz.exe <integer>");
    Console.WriteLine("       fb.bat <integer>");
    return unit;
}

// Invokes a function over a range from 1 to input
Unit For1To(int input, Func<Unit, int, Unit> fn) =>
    // Use Aggregate here because...
    // ...it hides the loop
    // ...it does not create another collection like Select
    Enumerable.Range(1, input).Aggregate(unit, fn);