using System.Security.Claims;
using System.Threading.Tasks;
using Gift_Of_The_Givers_Web_App.Data;
using Gift_Of_The_Givers_Web_App.Models;
using Gift_Of_The_Givers_Web_App.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<GiftOfTheGiversContext> _mockContext;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            // Create a mock of the GiftOfTheGiversContext
            var options = new DbContextOptionsBuilder<GiftOfTheGiversContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _mockContext = new Mock<GiftOfTheGiversContext>(options);
            _userService = new UserService(_mockContext.Object);
        }

        [Fact]
        public async Task GetCurrentUserAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = "user123";
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims);
            var userPrincipal = new ClaimsPrincipal(identity);

            var user = new User { Id = userId, Username = "Test User" };
            _mockContext.Setup(ctx => ctx.Users.FindAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetCurrentUserAsync(userPrincipal);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public async Task GetCurrentUserAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "user123";
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims);
            var userPrincipal = new ClaimsPrincipal(identity);

            // Simulate no user found in the context
            _mockContext.Setup(ctx => ctx.Users.FindAsync(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.GetCurrentUserAsync(userPrincipal);

            // Assert
            Assert.Null(result);
        }
    }
}
