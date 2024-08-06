using System.Text.RegularExpressions;

namespace PreludeTests;

[TestClass]
public class StringsTests
{
    [TestMethod]
    public void Strings_ErrorIfNullOrWhiteSpace_ReturnsUnitOnNonTrivialString()
    {
        // Act
        var result = "Good String".ErrorIfNullOrWhiteSpace();

        // Assert
        result.IsRight.Should().BeTrue();
        result.Value().Should().Be(unit);
    }

    [TestMethod]
    public void Strings_ErrorIfNullOrWhiteSpace_ReturnsErrorOnEmptyString()
    {
        // Act
        var result = string.Empty.ErrorIfNullOrWhiteSpace();

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("String is null or white space.");
    }

    [TestMethod]
    public void Strings_ErrorIfNullOrWhiteSpace_ReturnsCustomErrorOnEmptyString()
    {
        // Act
        var result = string.Empty.ErrorIfNullOrWhiteSpace("A custom error.");

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("A custom error.");
    }

    [TestMethod]
    public void Strings_ErrorIfDoesNotMatch_ReturnsUnitOnMatchingString()
    {
        // Act
        var regex = new Regex(".*");
        var result = "Good string".ErrorIfDoesNotMatch(regex);

        // Assert
        result.IsRight.Should().BeTrue();
        result.Value().Should().Be(unit);
    }

    [TestMethod]
    public void Strings_ErrorIfDoesNotMatch_ErrorsWhenStringDoesNotMatch()
    {
        // Act
        var regex = new Regex("^[a-z]+$");
        var result = "!a g#o%o^d str!ng".ErrorIfDoesNotMatch(regex);

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("String does not match regular expression.");
    }

    [TestMethod]
    public void Strings_ErrorIfDoesNotMatch_ReturnsCustomErrorWhenStringDoesNotMatch()
    {
        // Act
        var regex = new Regex("^[a-z]+$");
        var result = "!a g#o%o^d str!ng".ErrorIfDoesNotMatch(regex, "A custom error.");

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("A custom error.");
    }
}
