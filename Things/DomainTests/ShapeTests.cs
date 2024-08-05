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
        result.IsRight.Should().BeTrue();
        result.Value().Should().Be(Shape.Round);
    }

    [TestMethod]
    public void Prelude_ToShape_ReturnsUnableToParseError()
    {
        // Act
        var result = ToShape("xyz");

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("Unable to parse shape 'xyz'.");
    }
}
