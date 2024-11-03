using Gift_Of_The_Givers_Web_App.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class LoginViewModelTests
    {
        private List<ValidationResult> ValidateModel(LoginViewModel loginViewModel)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(loginViewModel, null, null);
            Validator.TryValidateObject(loginViewModel, validationContext, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void Email_ShouldBeRequired()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            {
                Email = string.Empty,
                Password = "Password123"
            };

            // Act
            var validationResults = ValidateModel(loginViewModel);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Email is required.");
        }

        [Fact]
        public void Email_ShouldHaveValidFormat()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            {
                Email = "invalid-email",
                Password = "Password123"
            };

            // Act
            var validationResults = ValidateModel(loginViewModel);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Invalid email address format.");
        }

        [Fact]
        public void Password_ShouldBeRequired()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            {
                Email = "test@example.com",
                Password = string.Empty
            };

            // Act
            var validationResults = ValidateModel(loginViewModel);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Password is required.");
        }

        [Fact]
        public void RememberMe_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            {
                Email = "test@example.com",
                Password = "Password123",
                RememberMe = true
            };

            // Act
            var validationResults = ValidateModel(loginViewModel);

            // Assert
            Assert.Empty(validationResults); // Should not have validation errors
        }
    }
}
