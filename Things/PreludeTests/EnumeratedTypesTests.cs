namespace PreludeTests;

[TestClass]
public class EnumeratedTypesTests
{
    [TestMethod]
    public void EnumeratedTypes_ParseEnum_ReturnsEnumeratedValue()
    {
        // Act
        var result = ParseEnum<Color>("red");

        // Assert
        result.IsRight.Should().BeTrue();
        result.Value().Should().Be(Color.Red);
    }

    [TestMethod]
    public void EnumeratedTypes_ParseEnum_ReturnsErrorWhenUnableToParse()
    {
        // Act
        var result = ParseEnum<Color>("yellow");

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("Unable to parse Color from value 'yellow'.");
    }

    private enum Color { Red, Green, Blue }
}