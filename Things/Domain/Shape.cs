namespace Things.Domain;

/// <summary>
/// Used the describe the shape of a thing
/// </summary>
public enum Shape
{
    /// <summary>
    /// Describes a thing that is round like a circle or oval
    /// </summary>
    Round,

    /// <summary>
    /// Describes a thing that is curvy like the letter 's'
    /// </summary>
    Curvy,

    /// <summary>
    /// Describes a thing that is angular like a polygon
    /// </summary>
    Angular
}

public static partial class Functions
{
    /// <summary>
    /// Attempts to convert a string into a shape
    /// </summary>
    /// <param name="shapeAsString">The input string</param>
    /// <returns>A shape or an error</returns>
    public static Either<Error, Shape> ToShape(string value) =>
        from shape in ParseEnum<Shape>(value)
        select shape;
}
