using Gift_Of_The_Givers_Web_App.Models;
using Xunit;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class ResourceTests
    {
        [Fact]
        public void Resource_CanBeInstantiated()
        {
            // Arrange
            var resource = new Resource();

            // Act & Assert
            Assert.NotNull(resource);
        }

        [Fact]
        public void Resource_ShouldSetProperties()
        {
            // Arrange
            var resource = new Resource
            {
                ResourceID = 1,
                Name = "Water Bottles",
                Description = "Bottled water for distribution.",
                Quantity = 100
            };

            // Act & Assert
            Assert.Equal(1, resource.ResourceID);
            Assert.Equal("Water Bottles", resource.Name);
            Assert.Equal("Bottled water for distribution.", resource.Description);
            Assert.Equal(100, resource.Quantity);
        }
    }
}
