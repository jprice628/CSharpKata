using static CSharpKata.Application;
using static CSharpKata.BinarySearch;

// The array that will be searched.
var arr = new int[] { 6, 9, 22, 24, 46, 52, 65, 76, 82, 96 };

PrintTitle("Running without logger...");
Run(from input in GetInput(args)
    let result = Search(arr, input)
    select ResultMessage(input, result));

Print("");

PrintTitle("Running with logger...");
Run(from input in GetInput(args)
    let result = SearchW(arr, input).Invoke()
    select ResultMessageW(input, result.Value, result.Output));

Print("");