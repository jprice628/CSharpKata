namespace Things;

public static partial class Prelude
{
    /// <summary>
    /// Ensures that a collection is not empty.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection</typeparam>
    /// <param name="collection">The collection</param>
    /// <param name="reason">The reason the collection should be non-empty</param>
    /// <returns>The specified collection if it is not empty; otherwise an error</returns>
    public static Either<Error, Unit> ErrorIfEmpty<T>(IEnumerable<T> collection, string? errorMessage = null) =>
        !collection.Any() ? Error.New(errorMessage ?? "The specified collection cannot be empty.")
        : unit;

    /// <summary>
    /// Gets the first item from a collection
    /// </summary>
    /// <typeparam name="T">The type of items in the collection</typeparam>
    /// <param name="collection">The collection</param>
    /// <returns>The first item in the collection or an error</returns>
    public static Either<Error, T> FirstItem<T>(this IEnumerable<T> collection) =>
        from _ in ErrorIfEmpty(collection, "Cannot get the head of an empty collection.")
        select collection.First();


    /// <summary>
    /// Invokes a unit function for each item in a collection until the entire collection has been processed or an error occurs.
    /// </summary>
    /// <typeparam name="TSource">The type of the source collection</typeparam>
    /// <param name="source">The source collection</param>
    /// <param name="func">The function to invoke for each item in the source collection</param>
    /// <returns>A unit or an error</returns>
    public static Either<Error, Unit> WhileSuccess<TSource>(this IEnumerable<TSource> source, Func<TSource, Either<Error, Unit>> func)
    {
        foreach (var item in source)
        {
            var result = func(item);

            if (result.Case is Error err)
            {
                return err;
            }
        }

        return unit;
    }
}
