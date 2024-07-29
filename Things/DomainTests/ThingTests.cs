using Domain;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTests;

[TestClass]
public class ThingTests
{
    [TestMethod]
    public void Thing_New_CreatesNewThing()
    {
        // Act
        var size = Size.New(5).Value();
        var thing = Thing.New(Shape.Curvy, size);

        // Assert
        thing.Id.Value.Should().NotBe(Guid.Empty);
        thing.Shape.Should().Be(Shape.Curvy);
        thing.Size.Value.Should().Be(5);
        thing.Changes.Should().HaveCount(1);
        thing.Changes.First().Should().BeOfType<CreatedEvent>();

        var created = (CreatedEvent)thing.Changes.First();
        created.ThingId.Should().Be(thing.Id);
        created.Shape.Should().Be(Shape.Curvy);
        created.Size.Value.Should().Be(5);
        created.Timestamp.Value.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(2));
    }

    [TestMethod]
    public void Thing_New_ReconstitutesThingFromEvents()
    {
        var createdTime = Timestamp.New("7/29/2024 1:00:00 PM +00:00").Value();
        var originalSize = Size.New(5).Value();
        var created = new CreatedEvent(createdTime, ThingId.New(), Shape.Round, originalSize);

        var reshapedTime = Timestamp.New("7/29/2024 2:00:00 PM +00:00").Value();
        var reshaped = new ReshapedEvent()
    }

    [TestMethod]
    public void Thing_Reshape_ReturnsUnchanged()
    {
        // Arrange
        var size = Size.New(5).Value();
        var original = Thing.New(Shape.Curvy, size);

        // Act
        var thing = original.Reshape(Shape.Curvy);

        // Assert
        thing.Should().BeSameAs(original);
    }

    [TestMethod]
    public void Thing_Reshape_ChangesShape()
    {
        // Arrange
        var size = Size.New(5).Value();
        var original = Thing.New(Shape.Curvy, size);

        // Act
        var thing = original.Reshape(Shape.Angular);

        // Assert
        thing.Should().NotBeSameAs(original);
        thing.Shape.Should().Be(Shape.Angular);
        thing.Changes.Should().HaveCount(2);
        thing.Changes[1].Should().BeOfType<ReshapedEvent>();

        var reshaped = (ReshapedEvent)thing.Changes[1];
        reshaped.NewShape.Should().Be(Shape.Angular);
        reshaped.Timestamp.Value.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(2));
    }

    [TestMethod]
    public void Thing_Resize_ReturnsUnchanged()
    {
        // Arrange
        var size = Size.New(5).Value();
        var original = Thing.New(Shape.Curvy, size);

        // Act
        var newSize = Size.New(5).Value();
        var thing = original.Resize(newSize);

        // Assert
        thing.Should().BeSameAs(original);
    }

    [TestMethod]
    public void Thing_Resize_ChangesSize()
    {
        // Arrange
        var size = Size.New(5).Value();
        var original = Thing.New(Shape.Curvy, size);

        // Act
        var newSize = Size.New(8).Value();
        var thing = original.Resize(newSize);

        // Assert
        thing.Should().NotBeSameAs(original);
        thing.Size.Should().Be(newSize);
        thing.Changes.Should().HaveCount(2);
        thing.Changes[1].Should().BeOfType<ResizedEvent>();

        var resized = (ResizedEvent)thing.Changes[1];
        resized.NewSize.Should().Be(newSize);
        resized.Timestamp.Value.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(2));
    }
}
