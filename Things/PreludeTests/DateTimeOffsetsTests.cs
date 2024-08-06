namespace PreludeTests;

[TestClass]
public class DateTimeOffsetsTests
{
    [TestMethod]
    public void DateTimeOffsets_ParseDateTimeOffset_ReturnsDateTimeOffset()
    {
        // Act
        var str = "7/29/2024 3:17:48 AM +00:00";
        var result = ParseDateTimeOffset(str);

        // Assert
        result.IsRight.Should().BeTrue();
        result.Value().Should().Be(DateTimeOffset.Parse(str));
    }

    [TestMethod]
    public void DateTimeOffsets_ParseDateTimeOffset_ReturnsDefaultError()
    {
        // Act
        var str = "abc123";
        var result = ParseDateTimeOffset(str);

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("Unable to parse timestamp.");
    }

    [TestMethod]
    public void DateTimeOffsets_ParseDateTimeOffset_ReturnsCustomError()
    {
        // Act
        var str = "abc123";
        var result = ParseDateTimeOffset(str, "A custom error");

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("A custom error");
    }
}
