Run(from input in GetInput(args)
    let _ = For1To(input, (_, n) => Print(FizzBuzz(n)))
    select unit);

Unit Run(Fin<Unit> fin) => fin.Match(
    Succ: _ => Print("Program complete!"),
    Fail: PrintError);

Fin<int> GetInput(string[] args) =>
    args.Length != 1 ? Error.New("Incorrect number of arguments provided.")
    : string.IsNullOrWhiteSpace(args[0]) ? Error.New("Argument is null or white space.")
    : !int.TryParse(args[0], out var result) ? Error.New("Unable to parse argument value")
    : result < 1 ? Error.New("Argument is less than one.")
    : result;

string FizzBuzz(int n) =>
    new StringBuilder($"{n} ")
    .Append(Fizz(n))
    .Append(Buzz(n))
    .ToString().Trim();

string Fizz(int n) => 
    n % 3 == 0 ? "Fizz" : string.Empty;

string Buzz(int n) => 
    n % 5 == 0 ? "Buzz" : string.Empty;

Unit Print(string text)
{
    Console.WriteLine(text);
    return unit;
}

Unit PrintError(Error err)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Error: " + err.Message);
    Console.ResetColor();
    Console.WriteLine("Usage: FizzBuzz.exe <integer>");
    Console.WriteLine("       fb.bat <integer>");
    return unit;
}

Unit For1To(int input, Func<Unit, int, Unit> fn) =>
    // Use Aggregate here because it abstracts away the loop without creating
    // another collection like Select.
    Enumerable.Range(1, input).Aggregate(unit, fn);