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
        result.IsRight.Should().BeTrue();
        result.Value().Value.Should().Be(5);
    }
}
