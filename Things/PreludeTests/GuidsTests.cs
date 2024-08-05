using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PreludeTests;

[TestClass]
public class GuidsTests
{
    [TestMethod]
    public void Prelude_ErrorIfEmpty_ReturnsUnitOnNonEmptyGuid()
    {
        // Act
        var guid = Guid.NewGuid();
        var result = guid.ErrorIfEmpty();

        // Assert
        result.IsRight.Should().BeTrue();
    }

    [TestMethod]
    public void Prelude_ErrorIfEmpty_ReturnsDefaultError()
    {
        // Act
        var guid = Guid.Empty;
        var result = guid.ErrorIfEmpty();

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("The supplied GUID is empty.");
    }

    [TestMethod]
    public void Prelude_ErrorIfEmpty_ReturnsCustomError()
    {
        // Act
        var guid = Guid.Empty;
        var result = guid.ErrorIfEmpty("A custom error message.");

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("A custom error message.");
    }
}
