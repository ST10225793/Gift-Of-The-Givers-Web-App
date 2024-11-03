using Gift_Of_The_Givers_Web_App.Models;
using System;
using Xunit;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class UserContributionTests
    {
        [Fact]
        public void UserContribution_CanBeInstantiated()
        {
            // Arrange & Act
            var contribution = new UserContribution();

            // Assert
            Assert.NotNull(contribution);
        }

        [Fact]
        public void UserContribution_ShouldSetProperties()
        {
            // Arrange
            var contribution = new UserContribution
            {
                ContributionID = 1,
                UserID = "user-123",
                TaskName = "Distribute Food",
                Date = DateTime.UtcNow,
                HoursContributed = 5,
                Status = "Completed",
                User = new User // Assuming you have a User class that can be instantiated
                {
                    UserID = 4,
                    Username = "marry poppin"
                }
            };

            // Act & Assert
            Assert.Equal(1, contribution.ContributionID);
            Assert.Equal("user-123", contribution.UserID);
            Assert.Equal("Distribute Food", contribution.TaskName);
            Assert.True((DateTime.UtcNow - contribution.Date).TotalSeconds < 1); // Check if Date is close to now
            Assert.Equal(5, contribution.HoursContributed);
            Assert.Equal("Completed", contribution.Status);
            Assert.NotNull(contribution.User);
            Assert.Equal("marry poppin", contribution.User.Username);
        }
    }
}
