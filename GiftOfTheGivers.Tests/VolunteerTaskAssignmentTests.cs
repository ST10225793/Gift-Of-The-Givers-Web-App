using Gift_Of_The_Givers_Web_App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class VolunteerTaskAssignmentTests
    {
        [Fact]
        public void VolunteerTaskAssignment_CanBeInstantiated()
        {
            // Arrange & Act
            var assignment = new VolunteerTaskAssignment();

            // Assert
            Assert.NotNull(assignment);
        }

        [Fact]
        public void VolunteerTaskAssignment_ShouldSetUserID()
        {
            // Arrange
            var assignment = new VolunteerTaskAssignment();
            var userId = "user123";

            // Act
            assignment.UserID = userId;

            // Assert
            Assert.Equal(userId, assignment.UserID);
        }

        [Fact]
        public void VolunteerTaskAssignment_ShouldSetTaskID()
        {
            // Arrange
            var assignment = new VolunteerTaskAssignment();
            var taskId = 1;

            // Act
            assignment.TaskID = taskId;

            // Assert
            Assert.Equal(taskId, assignment.TaskID);
        }

        [Fact]
        public void VolunteerTaskAssignment_ShouldSetAssignedDate()
        {
            // Arrange
            var assignment = new VolunteerTaskAssignment();
            var assignedDate = new DateTime(2024, 11, 10);

            // Act
            assignment.AssignedDate = assignedDate;

            // Assert
            Assert.Equal(assignedDate, assignment.AssignedDate);
        }

        [Fact]
        public void VolunteerTaskAssignment_ShouldSetTask()
        {
            // Arrange
            var assignment = new VolunteerTaskAssignment();
            var task = new VolunteerTask { TaskID = 1, Name = "Food Distribution" };

            // Act
            assignment.Task = task;

            // Assert
            Assert.Equal(task, assignment.Task);
            Assert.Equal(task.TaskID, assignment.Task.TaskID);
        }

        [Fact]
        public void VolunteerTaskAssignment_Validation_ShouldFail_WhenUserIDIsNull()
        {
            // Arrange
            var assignment = new VolunteerTaskAssignment
            {
                TaskID = 1,
                AssignedDate = DateTime.Now,
                UserID = null // Invalid UserID
            };

            // Act
            var context = new ValidationContext(assignment);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(assignment, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "The UserID field is required.");
        }

        [Fact]
        public void VolunteerTaskAssignment_Validation_ShouldFail_WhenTaskIDIsZero()
        {
            // Arrange
            var assignment = new VolunteerTaskAssignment
            {
                UserID = "user123",
                TaskID = 0, // Explicitly set to zero
                AssignedDate = DateTime.Now // Provide a valid date
            };

            // Act
            var context = new ValidationContext(assignment);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(assignment, context, results, true);

            // Assert
            Assert.False(isValid);  // Expecting validation to fail
            Assert.Contains(results, r => r.ErrorMessage == "The TaskID must be greater than 0."); // Check the error message
        }


        [Fact]
        public void VolunteerTaskAssignment_Validation_ShouldFail_WhenAssignedDateIsDefault()
        {
            // Arrange
            var assignment = new VolunteerTaskAssignment
            {
                UserID = "user123",
                TaskID = 1,
                AssignedDate = DateTime.MinValue // Explicitly set to default
            };

            // Act
            var context = new ValidationContext(assignment);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(assignment, context, results, true);

            // Assert
            Assert.False(isValid);  // Expecting validation to fail
            Assert.Contains(results, r => r.ErrorMessage == "The AssignedDate must be a valid date.");
        }




    }
}
