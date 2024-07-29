using LanguageExt;
using LanguageExt.Common;

namespace Domain;

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

public static partial class Prelude
{
    /// <summary>
    /// Attempts to convert a string into a shape
    /// </summary>
    /// <param name="s">The input string</param>
    /// <returns>A shape or an error</returns>
    public static Fin<Shape> ToShape(string s) =>
        !Enum.TryParse<Shape>(s, true, out var shape) ? Error.New("Prelude: Unable to parse shape.")
        : shape;
}
