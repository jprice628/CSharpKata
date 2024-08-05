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
}
