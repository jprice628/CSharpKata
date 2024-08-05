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
        result.IsRight.Should().BeTrue();
        result.Value().Value.Should().Be(guid);
    }

    [TestMethod]
    public void ThingId_New_ReturnsThingIdFromString()
    {
        // Act
        var guidAsString = "aaa7f552-7607-4cf9-bd01-d9fa7f470be2";
        var result = ThingId.New(guidAsString);

        // Assert
        result.IsRight.Should().BeTrue();
        result.Value().Value.Should().Be(new Guid(guidAsString));
    }

    [TestMethod]
    public void ThingId_New_ReturnsRandomThingId()
    {
        // Act
        var id1 = ThingId.New();
        var id2 = ThingId.New();

        // Assert
        id1.Value.Should().NotBe(Guid.Empty);
        id2.Value.Should().NotBe(Guid.Empty);
        id1.Value.Should().NotBe(id2.Value);
    }
}