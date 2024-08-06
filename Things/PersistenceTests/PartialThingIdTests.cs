using Microsoft.VisualStudio.TestTools.UnitTesting;
using Things.Domain;
using Things.Persistence;

namespace PersistenceTests;

[TestClass]
public class PartialThingIdTests
{
    [TestMethod]
    public void PartialThingId_New_CreatedPartialThingIdFromThingId()
    {
        // Act
        var thingId = ThingId.New("8a79166e-fc40-4b01-b3f3-964653008fa3").Value();
        var partialId = PartialThingId.New(thingId);

        // Assert
        partialId.Value.Should().Be("8a79166e");
    }

    [TestMethod]
    public void PartialThingId_New_CreatesPartialThingIdFromString()
    {
        // Act
        var result = PartialThingId.New("abcd1234");

        // Assert
        result.IsRight.Should().BeTrue();
        result.Value().Value.Should().Be("abcd1234");
    }
}
