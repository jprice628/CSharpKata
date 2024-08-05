using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PreludeTests;

[TestClass]
public class CollectionsTests
{
    [TestMethod]
    public void Prelude_ErrorIfEmpty_ReturnsNonEmptyCollection()
    {
        // Act
        var collection = new[] { 1, 2, 3 };
        var result = ErrorIfEmpty(collection);

        // Assert
        result.IsRight.Should().BeTrue();
        result.Value().Should().Be(unit);
    }

    [TestMethod]
    public void Prelude_ErrorIfEmpty_ReturnsDefaultErrorWhenCollectionIsEmpty()
    {
        // Act
        var collection = System.Array.Empty<int>();
        var result = ErrorIfEmpty(collection);

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("The specified collection cannot be empty.");
    }

    [TestMethod]
    public void Prelude_ErrorIfEmpty_ReturnsCustomErrorWhenCollectionIsEmpty()
    {
        // Act
        var collection = System.Array.Empty<int>();
        var result = ErrorIfEmpty(collection, "The test collection should not be empty.");

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("The test collection should not be empty.");
    }

    [TestMethod]
    public void Prelude_FirstItem_ReturnsFirstItemFromTheCollection()
    {
        // Act
        var collection = new[] { 1, 2, 3, 4 };
        var result = collection.FirstItem();

        // Assert
        result.IsRight.Should().BeTrue();
        result.Value().Should().Be(1);
    }
}
