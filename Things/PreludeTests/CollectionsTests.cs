using Microsoft.VisualStudio.TestTools.UnitTesting;
using Things;

namespace PreludeTests;

[TestClass]
public class CollectionsTests
{
    [TestMethod]
    public void Collections_ErrorIfEmpty_ReturnsNonEmptyCollection()
    {
        // Act
        var collection = new[] { 1, 2, 3 };
        var result = ErrorIfEmpty(collection);

        // Assert
        result.IsRight.Should().BeTrue();
        result.Value().Should().Be(unit);
    }

    [TestMethod]
    public void Collections_ErrorIfEmpty_ReturnsDefaultErrorWhenCollectionIsEmpty()
    {
        // Act
        var collection = System.Array.Empty<int>();
        var result = ErrorIfEmpty(collection);

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("The specified collection cannot be empty.");
    }

    [TestMethod]
    public void Collections_ErrorIfEmpty_ReturnsCustomErrorWhenCollectionIsEmpty()
    {
        // Act
        var collection = System.Array.Empty<int>();
        var result = ErrorIfEmpty(collection, "The test collection should not be empty.");

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("The test collection should not be empty.");
    }

    [TestMethod]
    public void Collections_FirstItem_ReturnsFirstItemFromTheCollection()
    {
        // Act
        var collection = new[] { 1, 2, 3, 4 };
        var result = collection.FirstItem();

        // Assert
        result.IsRight.Should().BeTrue();
        result.Value().Should().Be(1);
    }

    [TestMethod]
    public void Collections_WhileSuccess_IteratesOverEntireCollection()
    {
        // Arrange
        var list = new List<int>();
        Either<Error, Unit> AddToList(int n) { list.Add(n); return unit; }

        // Act
        var collection = new[] { 1, 2, 3 };
        var result = collection.WhileSuccess(AddToList);

        // Assert
        result.IsRight.Should().BeTrue();
        list.Should().BeEquivalentTo(collection);
    }

    [TestMethod]
    public void Collections_WhileSuccess_FailsFast()
    {
        // Arrange
        var list = new List<int>();
        
        Either<Error, Unit> AddOddsFailEvens(int n)
        {
            if (n % 2== 0) { return Error.New("Even!"); }
            else { list.Add(n); return unit; }
        }

        // Act
        var collection = new[] { 1, 2, 3 };
        var result = collection.WhileSuccess(AddOddsFailEvens);

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("Even!");
        list.Should().BeEquivalentTo(collection.Take(1));
    }
}
