using Domain;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests;

[TestClass]
public class TimestampTests
{
    [TestMethod]
    public void Timestamp_UtcNow_ReturnsUtcTimestamp()
    {
        // Act
        var ts = Timestamp.UtcNow;

        // Assert
        ts.Value.Offset.Should().Be(TimeSpan.Zero);
        ts.Value.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(2));
    }

    [TestMethod]
    public void Timestamp_New_ReturnsTimestampFromString()
    {
        // Act
        var str = "7/29/2024 3:17:48 AM +00:00";
        var result = Timestamp.New(str);

        // Assert
        result.IsSucc.Should().BeTrue();
        result.Value().Value.Should().Be(DateTimeOffset.Parse(str));
    }

    [TestMethod]
    public void Timestamp_New_ReturnsUnableToParseError()
    {
        // Act
        var str = "xyz";
        var result = Timestamp.New(str);

        // Assert
        result.IsSucc.Should().BeFalse();
        result.Error().Message.Should().Be("Timestamp: Unable to parse string.");
    }

    [TestMethod]
    public void Timestamp_ToUniversalTime_ReturnsUtcTimestamp()
    {
        // Act
        var dt = DateTimeOffset.Parse("7/29/2024 12:00:00 PM -04:00");
        var ts = new Timestamp(dt).ToUniversalTime();

        // Assert
        ts.Value.Offset.Should().Be(TimeSpan.Zero);
        ts.Value.Should().Be(DateTimeOffset.Parse("7/29/2024 04:00:00 PM +00:00"));
    }
}
