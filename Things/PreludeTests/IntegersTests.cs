using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PreludeTests;

[TestClass]
public class IntegersTests
{
    [TestMethod]
    public void Prelude_Between_ReturnsValidIntegerValue()
    {
        // Act
        var result = Between(5, 1, 10);

        // Assert
        result.IsRight.Should().BeTrue();
        result.Value().Should().Be(5);
    }

    [TestMethod]
    public void Prelude_Between_ReturnsErrorOnLowValue()
    {
        // Act
        var result = Between(-1, 1, 10);

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("Value cannot be less than 1.");
    }

    [TestMethod]
    public void Prelude_Between_ReturnsErrorOnHighValue()
    {
        // Act
        var result = Between(20, 1, 10);

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("Value cannot be greater than 10.");
    }
}
