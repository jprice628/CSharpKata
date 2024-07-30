using FluentAssertions;

namespace DomainTests;

[TestClass]
public class ShapeTests
{
    [TestMethod]
    public void Prelude_ToShape_ReturnsShape()
    {
        // Act
        var result = ToShape("round");

        // Assert
        result.IsSucc.Should().BeTrue();
        result.Value().Should().Be(Shape.Round);
    }

    [TestMethod]
    public void Prelude_ToShape_ReturnsUnableToParseError()
    {
        // Act
        var result = ToShape("xyz");

        // Assert
        result.IsSucc.Should().BeFalse();
        result.Error().Message.Should().Be("Prelude: Unable to parse shape.");
    }
}
