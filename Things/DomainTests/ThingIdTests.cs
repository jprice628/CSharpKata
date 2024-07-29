using Domain;
using FluentAssertions;

namespace DomainTests;

[TestClass]
public class ThingIdTests
{
    [TestMethod]
    public void ThingId_New_ReturnsThingIdFromGuid()
    {
        var guid = new Guid("aaa7f552-7607-4cf9-bd01-d9fa7f470be2");
        var result = ThingId.New(guid);

        result.IsSucc.Should().BeTrue();
        result.Value().Value.Should().Be(guid);
    }

    [TestMethod]
    public void ThingId_New_ReturnsThingIdFromString()
    {
        var guidAsString = "aaa7f552-7607-4cf9-bd01-d9fa7f470be2";
        var result = ThingId.New(guidAsString);

        result.IsSucc.Should().BeTrue();
        result.Value().Value.Should().Be(new Guid(guidAsString));
    }

    [TestMethod]
    public void ThingId_New_ReturnsEmptyGuidError()
    {
        var result = ThingId.New(Guid.Empty);

        result.IsSucc.Should().BeFalse();
        result.Error().Message.Should().Be("ThingId: Value cannot be an empty GUID.");
    }

    [TestMethod]
    public void ThingId_New_ReturnsUnableToParseError()
    {
        var guidAsString = "xyz123";
        var result = ThingId.New(guidAsString);

        result.IsSucc.Should().BeFalse();
        result.Error().Message.Should().Be("ThingId: Unable to parse GUID.");
    }
}