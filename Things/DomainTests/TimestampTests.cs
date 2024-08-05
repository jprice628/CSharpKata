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
        result.IsRight.Should().BeTrue();
        result.Value().Value.Should().Be(DateTimeOffset.Parse(str));
    }

    [TestMethod]
    public void Timestamp_New_ReturnsUnableToParseError()
    {
        // Act
        var str = "xyz";
        var result = Timestamp.New(str);

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("Unable to parse timestamp.");
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

    [TestMethod]
    public void Timestamp_Order_SortsTimestampsCorrectly()
    {
        var t1 = Timestamp.New("7/29/2024 1:00:00 PM +00:00").Value();
        var t2 = Timestamp.New("7/29/2024 2:00:00 PM +00:00").Value();
        var t3 = Timestamp.New("7/29/2024 3:00:00 PM +00:00").Value();

        var outOfOrder = new[] { t2, t3, t1 };
        var sorted = outOfOrder.Order().ToArray();

        sorted[0].Should().Be(t1);
        sorted[1].Should().Be(t2);
        sorted[2].Should().Be(t3);
    }
}
