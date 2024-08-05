namespace Things.Persistence;

internal static class IO
{
    internal static Either<Error, Unit> WriteToFile(FileInfo file, IEnumerable<string> lines) =>
        Try(() => { File.WriteAllLines(file.FullName, lines); return unit; })
        .ToEither(ex => Error.New("An error occurred while attempting to write to a file: " + ex.Message));

    internal static EitherAsync<Error, Unit> WriteToFileAsync(FileInfo file, IEnumerable<string> lines, CancellationToken cancellationToken = default) =>
        TryAsync(async () => { await File.WriteAllLinesAsync(file.FullName, lines, cancellationToken); return unit; })
        .ToEither(err => Error.New("An error occurred while attempting to write to a file: " + err.Message));

    internal static Either<Error, string[]> ReadFromFile(FileInfo file) =>
        Try(() => File.ReadAllLines(file.FullName))
        .ToEither(ex => Error.New("An error occurred while attempting to read from a file: " + ex.Message));

    internal static EitherAsync<Error, string[]> ReadFromFileAsync(FileInfo file, CancellationToken cancellationToken = default) =>
        TryAsync(() => File.ReadAllLinesAsync(file.FullName, cancellationToken))
        .ToEither(err => Error.New("An error occurred while attempting to read from a file: " + err.Message));    

    internal static Either<Error, FileInfo[]> FindFiles(DirectoryInfo directory, string searchPattern) =>
        Try(() => directory.GetFiles(searchPattern))
        .ToEither(ex => Error.New("An error occurred while attempting to find files: " + ex.Message));

    internal static Either<Error, FileInfo> FindOneFile(DirectoryInfo storageDirectory, string searchPattern) =>
        from files in FindFiles(storageDirectory, searchPattern)
        from single in Single(files)
        select single;

    private static Either<Error, FileInfo> Single(FileInfo[] files) =>
        files.Length < 1 ? Error.New("No files found.")
        : files.Length > 1 ? Error.New("Multiple files found.")
        : files[0];
}
