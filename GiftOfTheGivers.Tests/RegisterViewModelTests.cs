using Gift_Of_The_Givers_Web_App.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class RegisterViewModelTests
    {
        private List<ValidationResult> ValidateModel(RegisterViewModel registerViewModel)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(registerViewModel, null, null);
            Validator.TryValidateObject(registerViewModel, validationContext, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void Username_ShouldBeRequired()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            {
                Username = string.Empty,
                Email = "test@example.com",
                Password = "Password123",
                ConfirmPassword = "Password123"
            };

            // Act
            var validationResults = ValidateModel(registerViewModel);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The Username field is required.");
        }

        [Fact]
        public void Email_ShouldBeRequired()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            {
                Username = "testuser",
                Email = string.Empty,
                Password = "Password123",
                ConfirmPassword = "Password123"
            };

            // Act
            var validationResults = ValidateModel(registerViewModel);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The Email field is required.");
        }

        [Fact]
        public void Email_ShouldHaveValidFormat()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            {
                Username = "testuser",
                Email = "invalid-email-format",
                Password = "Password123",
                ConfirmPassword = "Password123"
            };

            // Act
            var validationResults = ValidateModel(registerViewModel);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The Email field is not a valid e-mail address.");
        }

        [Fact]
        public void Password_ShouldBeRequired()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = string.Empty,
                ConfirmPassword = string.Empty
            };

            // Act
            var validationResults = ValidateModel(registerViewModel);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The Password field is required.");
        }

        [Fact]
        public void ConfirmPassword_ShouldMatchPassword()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "Password123",
                ConfirmPassword = "DifferentPassword"
            };

            // Act
            var validationResults = ValidateModel(registerViewModel);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The password and confirmation password do not match.");
        }

        [Fact]
        public void RegisterViewModel_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "Password123",
                ConfirmPassword = "Password123"
            };

            // Act
            var validationResults = ValidateModel(registerViewModel);

            // Assert
            Assert.Empty(validationResults); // Should not have validation errors
        }
    }
}
