using Gift_Of_The_Givers_Web_App.Models;
using Xunit;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class VolunteerTests
    {
        [Fact]
        public void Volunteer_CanBeInstantiated()
        {
            // Arrange & Act
            var volunteer = new Volunteer();

            // Assert
            Assert.NotNull(volunteer);
        }

        [Fact]
        public void Volunteer_ShouldSetProperties()
        {
            // Arrange
            var volunteer = new Volunteer
            {
                VolunteerID = 1,
                UserID = 101,
                Name = "Marry Poppin",
                Email = "marryp@example.com",
                Phone = "123-456-7890",
                Skills = "First Aid, Communication",
                User = new User 
                {
                    UserID = 4,
                    Username = "marry poppin"
                }
            };

            // Act & Assert
            Assert.Equal(1, volunteer.VolunteerID);
            Assert.Equal(101, volunteer.UserID);
            Assert.Equal("Marry Poppin", volunteer.Name);
            Assert.Equal("marryp@example.com", volunteer.Email);
            Assert.Equal("123-456-7890", volunteer.Phone);
            Assert.Equal("First Aid, Communication", volunteer.Skills);
            Assert.NotNull(volunteer.User);
            Assert.Equal("marry poppin", volunteer.User.Username);
        }
    }
}
