using System;
using System.Collections.Generic;
using Xunit;
using Gift_Of_The_Givers_Web_App.Models;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class DonationTests
    {
        [Fact]
        public void Donation_Should_HaveRequiredFields()
        {
            // Arrange
            var donation = new Donation
            {
                DonationID = 1,
                UserID = 123,
                Item = "Food Supplies",
                Date = DateTime.Now,
                Quantity = 10,
                Status = "Pending"
            };

            // Act & Assert
            Assert.NotNull(donation.Item);
            Assert.True(donation.Quantity > 0);
            Assert.NotNull(donation.Status);
        }

        [Fact]
        public void Quantity_ShouldBePositive()
        {
            // Arrange
            var donation = new Donation { Quantity = 5 };

            // Act & Assert
            Assert.True(donation.Quantity > 0, "Quantity should be a positive number.");
        }

        [Fact]
        public void Date_ShouldNotBeInFuture()
        {
            // Arrange
            var donation = new Donation { Date = DateTime.Now.AddDays(-1) };

            // Act & Assert
            Assert.True(donation.Date <= DateTime.Now, "Date should not be in the future.");
        }

        [Fact]
        public void Status_ShouldBeValid()
        {
            // Arrange
            var donation = new Donation { Status = "Pending" };
            var validStatuses = new List<string> { "Pending", "Completed" };

            // Act & Assert
            Assert.Contains(donation.Status, validStatuses);
        }

        [Fact]
        public void Donation_Should_HaveDefaultAvailableResources()
        {
            // Arrange
            var donation = new Donation();

            // Act
            var availableResources = donation.AvailableResources;

            // Assert
            Assert.NotNull(availableResources);
            Assert.Empty(availableResources); // Default state should be an empty list
        }

        [Fact]
        public void Donation_Should_LinkToUser()
        {
            // Arrange
            var user = new User { UserId = 123, Username = "TestUser" };
            var donation = new Donation { Users = user };

            // Act & Assert
            Assert.Equal(user.UserId, donation.Users.UserId);
            Assert.Equal(user.Username, donation.Users.Username);
        }
    }
}
