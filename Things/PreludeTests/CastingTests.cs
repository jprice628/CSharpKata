namespace PreludeTests;

[TestClass]
public class CastingTests
{
    [TestMethod]
    public void Casting_As_ReturnsObjectOfCorrectType()
    {
        // Act
        object obj = this;
        var result = obj.As<CastingTests>(null);

        // Assert
        result.IsRight.Should().BeTrue();
        result.Value().Should().NotBeNull()
            .And.BeOfType<CastingTests>();
    }

    [TestMethod]
    public void Casting_As_ReturnsDefaultError()
    {
        // Act
        object obj = this;
        var result = obj.As<CollectionsTests>(null);

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("Unable to cast CastingTests to CollectionsTests.");
    }

    [TestMethod]
    public void Casting_As_ReturnsCustomError()
    {
        // Act
        object obj = this;
        var result = obj.As<CollectionsTests>("A custom error");

        // Assert
        result.IsRight.Should().BeFalse();
        result.Error().Message.Should().Be("A custom error");
    }
}
