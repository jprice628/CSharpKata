using LanguageExt;
using LanguageExt.Common;

namespace Things.Domain;

/// <summary>
/// Provides an expressive way to describe the size of a thing
/// </summary>
/// <remarks>
/// Thing sizes are values, so they benefit from being represented using records 
/// instead of classes. A record class is being used instead of a record struct
/// because the value has a universal constraint: it has to be between 1 and 10.
/// </remarks>
public sealed record Size
{
    /// <summary>
    /// The size value, a number between 1 and 10 (inclusive)
    /// </summary>
    public int Value { get; private init; }

    /// <summary>
    /// Constructs a new size value
    /// </summary>
    /// <param name="value">The integer value of the size</param>
    /// <remarks>
    /// This constructor expects the factory method below to validate the value
    /// </remarks>
    private Size(int value) =>
        Value = value;

    /// <summary>
    /// Constructs a new size value
    /// </summary>
    /// <param name="value">An integer from 1 to 10 (inclusive)</param>
    /// <returns>A size value or an error</returns>
    public static Fin<Size> New(int value) =>
        value < 1 ? Error.New("Size: value cannot be less than 1.")
        : value > 10 ? Error.New("Size: value cannot be greater than 10.")
        : new Size(value);
}
