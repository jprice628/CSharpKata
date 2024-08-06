using static Things.Persistence.IO;
using static Things.Persistence.Serialization;

namespace Things.Persistence;

internal class FileSystemThingRepository : IThingRepository
{
    readonly DirectoryInfo storageDirectory;

    internal FileSystemThingRepository(DirectoryInfo storageDirectory)
        => this.storageDirectory = storageDirectory;

    public Either<Error, Unit> SaveChanges(Thing thing) =>
        thing.Changes.Count < 1 ? unit
        : from _ in WriteToFile(GetFile(thing.Id), Serialize(thing.Changes))
          select unit;

    public EitherAsync<Error, Unit> SaveChangesAsync(Thing thing, CancellationToken cancellationToken = default) =>
        thing.Changes.Count < 1 ? unit
        : from _ in WriteToFileAsync(GetFile(thing.Id), Serialize(thing.Changes), cancellationToken)
          select unit;

    public Either<Error, Thing> GetById(ThingId id) =>
        from lines in ReadFromFile(GetFile(id))
        from events in Deserialize(lines)
        from thing in Thing.New(events)
        select thing;

    public EitherAsync<Error, Thing> GetByIdAsync(ThingId id, CancellationToken cancellationToken = default) =>
        from lines in ReadFromFileAsync(GetFile(id), cancellationToken)
        from events in Deserialize(lines).ToAsync()
        from thing in Thing.New(events).ToAsync()
        select thing;

    public Either<Error, Thing> GetByPartialId(PartialThingId partialId) =>
        from file in FindOneFile(storageDirectory, GetSearchPattern(partialId))
        from lines in ReadFromFile(file)
        from events in Deserialize(lines)
        from thing in Thing.New(events)
        select thing;

    public EitherAsync<Error, Thing> GetByPartialIdAsync(PartialThingId partialId, CancellationToken cancellationToken = default) =>
        from file in FindOneFile(storageDirectory, GetSearchPattern(partialId)).ToAsync()
        from lines in ReadFromFileAsync(file, cancellationToken)
        from events in Deserialize(lines).ToAsync()
        from thing in Thing.New(events).ToAsync()
        select thing;

    public Either<Error, IEnumerable<ThingId>> ListIds() =>
        from files in FindFiles(storageDirectory, "Thing-*.json")
        from ids in ToThingIds(files)
        select ids;

    private static Either<Error, IEnumerable<ThingId>> ToThingIds(FileInfo[] files) =>
        files.Select(ToThingId).Sequence();

    private static Either<Error, ThingId> ToThingId(FileInfo file) =>
        ThingId.New(file.Name[6..^5]);

    private static string GetSearchPattern(PartialThingId partialId) =>
        $"Thing-{partialId.Value}*.json";

    private FileInfo GetFile(ThingId thingId) =>
        new(Path.Combine(storageDirectory.FullName, $"Thing-{thingId.Value}.json"));
}