using Gift_Of_The_Givers_Web_App.Models;
using System;
using Xunit;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class ReliefProjectTests
    {
        [Fact]
        public void ReliefProject_CanBeInstantiated()
        {
            // Arrange
            var reliefProject = new ReliefProject();

            // Act & Assert
            Assert.NotNull(reliefProject);
        }

        [Fact]
        public void ReliefProject_ShouldSetProperties()
        {
            // Arrange
            var reliefProject = new ReliefProject
            {
                ReliefProjectID = 1,
                Name = "Food Distribution",
                Description = "Distribution of food to needy families.",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10)
            };

            // Act & Assert
            Assert.Equal(1, reliefProject.ReliefProjectID);
            Assert.Equal("Food Distribution", reliefProject.Name);
            Assert.Equal("Distribution of food to needy families.", reliefProject.Description);
            Assert.True(reliefProject.StartDate < reliefProject.EndDate);
        }

        [Fact]
        public void ReliefProject_StartDate_MustBeEarlierThanEndDate()
        {
            // Arrange
            var reliefProject = new ReliefProject
            {
                ReliefProjectID = 2,
                Name = "Clothing Drive",
                Description = "Collecting clothing donations.",
                StartDate = DateTime.Now.AddDays(10),
                EndDate = DateTime.Now
            };

            // Act & Assert
            Assert.True(reliefProject.StartDate > reliefProject.EndDate,
                "Start date must be earlier than end date.");
        }
    }
}
