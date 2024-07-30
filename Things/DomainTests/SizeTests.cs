using FluentAssertions;

namespace DomainTests;

[TestClass]
public class SizeTests
{
    [TestMethod]
    public void Size_New_ReturnsASizeValue()
    {
        // Act
        var result = Size.New(5);

        // Assert
        result.IsSucc.Should().BeTrue();
        result.Value().Value.Should().Be(5);
    }

    [TestMethod]
    public void Size_New_ReturnsSizeCannotBeLessThan1Error()
    {
        // Act
        var result = Size.New(0);

        // Assert
        result.IsSucc.Should().BeFalse();
        result.Error().Message.Should().Be("Size: value cannot be less than 1.");
    }

    [TestMethod]
    public void Size_New_ReturnsSizeCannotGreaterThan10Error()
    {
        // Act
        var result = Size.New(11);

        // Assert
        result.IsSucc.Should().BeFalse();
        result.Error().Message.Should().Be("Size: value cannot be greater than 10.");
    }
}
