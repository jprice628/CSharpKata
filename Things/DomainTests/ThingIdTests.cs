using Domain;
using FluentAssertions;

namespace DomainTests;

[TestClass]
public class ThingIdTests
{
    [TestMethod]
    public void ThingId_New_ReturnsThingIdFromGuid()
    {
        // Act
        var guid = new Guid("aaa7f552-7607-4cf9-bd01-d9fa7f470be2");
        var result = ThingId.New(guid);

        // Assert
        result.IsSucc.Should().BeTrue();
        result.Value().Value.Should().Be(guid);
    }

    [TestMethod]
    public void ThingId_New_ReturnsThingIdFromString()
    {
        // Act
        var guidAsString = "aaa7f552-7607-4cf9-bd01-d9fa7f470be2";
        var result = ThingId.New(guidAsString);

        // Assert
        result.IsSucc.Should().BeTrue();
        result.Value().Value.Should().Be(new Guid(guidAsString));
    }

    [TestMethod]
    public void ThingId_New_ReturnsEmptyGuidError()
    {
        // Act
        var result = ThingId.New(Guid.Empty);

        // Assert
        result.IsSucc.Should().BeFalse();
        result.Error().Message.Should().Be("ThingId: Value cannot be an empty GUID.");
    }

    [TestMethod]
    public void ThingId_New_ReturnsUnableToParseError()
    {
        // Act
        var guidAsString = "xyz123";
        var result = ThingId.New(guidAsString);

        // Assert
        result.IsSucc.Should().BeFalse();
        result.Error().Message.Should().Be("ThingId: Unable to parse GUID.");
    }
}