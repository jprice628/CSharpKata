namespace Things.Persistence;

internal static class IO
{
    internal static Try<Unit> WriteToFile(string path, IEnumerable<string> lines) =>
        Try(() => { File.WriteAllLines(path, lines); return unit; });

    internal static TryAsync<Unit> WriteToFileAsync(string path, IEnumerable<string> lines, CancellationToken cancellationToken = default) =>
        TryAsync(async () => { await File.WriteAllLinesAsync(path, lines, cancellationToken); return unit; });

    internal static Try<string[]> ReadFromFile(string path) =>
        Try(() => File.ReadAllLines(path));

    internal static TryAsync<string[]> ReadFromFile(string path, CancellationToken cancellationToken = default) =>
        TryAsync(() => File.ReadAllLinesAsync(path, cancellationToken));

    internal static Try<FileInfo[]> Find(DirectoryInfo storageDirectory, string searchPattern) =>
        Try(() => storageDirectory.GetFiles(searchPattern));
}
