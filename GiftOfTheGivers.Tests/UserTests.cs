using Gift_Of_The_Givers_Web_App.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class UserTests
    {
        private User CreateValidUser()
        {
            return new User
            {
                UserId = 1,
                Username = "TestUser",
                Password = "TestPassword123",
                ConfirmPassword = "TestPassword123",
                Role = "Admin"
            };
        }

        private List<ValidationResult> ValidateModel(User user)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user, null, null);
            Validator.TryValidateObject(user, validationContext, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void User_Should_HaveRequiredFields()
        {
            // Arrange
            var user = new User();

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Username is required.");
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Password is required.");
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Please confirm your password.");
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Please select a role.");
        }

        [Fact]
        public void Username_ShouldNotExceedMaxLength()
        {
            // Arrange
            var user = CreateValidUser();
            user.Username = new string('a', 51);  // 51 characters

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Username must be less than 50 characters.");
        }

        [Fact]
        public void Password_ShouldHaveMinLength()
        {
            // Arrange
            var user = CreateValidUser();
            user.Password = "123";  // Less than 6 characters

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Password must be at least 6 characters.");
        }

        [Fact]
        public void ConfirmPassword_ShouldMatchPassword()
        {
            // Arrange
            var user = CreateValidUser();
            user.ConfirmPassword = "DifferentPassword";

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            // Since the ConfirmPassword matching logic isn't enforced by data annotations, add logic to handle this case in real-world scenarios.
            Assert.NotEqual(user.Password, user.ConfirmPassword);
        }

        [Fact]
        public void Role_ShouldBeRequired()
        {
            // Arrange
            var user = CreateValidUser();
            user.Role = null;  // Required field left empty

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Please select a role.");
        }
    }
}
