namespace PreludeTests;

[TestClass]
public class IntegersTests
{
    [TestMethod]
    public void Prelude_Between_ReturnsValidIntegerValue()
    {
        // Act
        var result = 5.ErrorIfNotBetween(1, 10);

        // Assert
        result.IsRight.Should().BeTrue();
        result.Value().Should().Be(unit);
    }

    [TestMethod]
    public void Prelude_Between_ReturnsDefaultErrorOnLowValue()
    {
        // Act
        var result = (-1).ErrorIfNotBetween(1, 10);

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("The specified string must be between 1 and 10.");
    }

    [TestMethod]
    public void Prelude_Between_ReturnsDefaultErrorOnHighValue()
    {
        // Act
        var result = 20.ErrorIfNotBetween(1, 10);

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("The specified string must be between 1 and 10.");
    }

    [TestMethod]
    public void Prelude_Between_ReturnsCustomErrors()
    {
        // Act
        var result = 20.ErrorIfNotBetween(1, 10, "The test value must be between 1 and 10.");

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("The test value must be between 1 and 10.");
    }
}
