using Gift_Of_The_Givers_Web_App.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class VolunteerTaskTests
    {
        [Fact]
        public void VolunteerTask_CanBeInstantiated()
        {
            // Arrange & Act
            var task = new VolunteerTask();

            // Assert
            Assert.NotNull(task);
        }

        [Fact]
        public void VolunteerTask_ShouldSetName()
        {
            // Arrange
            var task = new VolunteerTask();
            var name = "Food Distribution";

            // Act
            task.Name = name;

            // Assert
            Assert.Equal(name, task.Name);
        }



        [Fact]
        public void VolunteerTask_ShouldSetDescription()
        {
            // Arrange
            var task = new VolunteerTask();
            var description = "Distributing food to the needy.";

            // Act
            task.Description = description;

            // Assert
            Assert.Equal(description, task.Description);
        }

        [Fact]
        public void VolunteerTask_ShouldSetDate()
        {
            // Arrange
            var task = new VolunteerTask();
            var date = new DateTime(2024, 11, 15);

            // Act
            task.Date = date;

            // Assert
            Assert.Equal(date, task.Date);
        }

        [Fact]
        public void VolunteerTask_ShouldSetStatus()
        {
            // Arrange
            var task = new VolunteerTask();
            var status = "Available";

            // Act
            task.Status = status;

            // Assert
            Assert.Equal(status, task.Status);
        }

        [Fact]
        public void VolunteerTask_Validation_ShouldFail_WhenNameIsNull()
        {
            // Arrange
            var task = new VolunteerTask
            {
                Name = null,
                Date = DateTime.Now,
                Status = "Available"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(task);
            bool isValid = Validator.TryValidateObject(task, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Name is required.");
        }

       
    }
}
