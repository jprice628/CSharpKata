using Domain;
using FluentAssertions;
using static Domain.Prelude;

namespace DomainTests;

[TestClass]
public class ShapeTests
{
    [TestMethod]
    public void Prelude_ToShape_ReturnsShape()
    {
        // Act
        var shape = ToShape("round");

        // Assert
        shape.IsSucc.Should().BeTrue();
        shape.Value().Should().Be(Shape.Round);
    }

    [TestMethod]
    public void Prelude_ToShape_ReturnsUnableToParseError()
    {
        // Act
        var shape = ToShape("xyz");

        // Assert
        shape.IsSucc.Should().BeFalse();
        shape.Error().Message.Should().Be("Prelude: Unable to parse shape.");
    }
}
