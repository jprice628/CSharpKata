
namespace Things.Persistence
{
    internal interface IThingRepository
    {
        Either<Error, Thing> GetById(ThingId id);
        EitherAsync<Error, Thing> GetByIdAsync(ThingId id, CancellationToken cancellationToken = default);
        Either<Error, Thing> GetByPartialId(PartialThingId partialId);
        EitherAsync<Error, Thing> GetByPartialIdAsync(PartialThingId partialId, CancellationToken cancellationToken = default);
        Either<Error, IEnumerable<ThingId>> ListIds();
        Either<Error, Unit> SaveChanges(Thing thing);
        EitherAsync<Error, Unit> SaveChangesAsync(Thing thing, CancellationToken cancellationToken = default);
    }
}