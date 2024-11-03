using Gift_Of_The_Givers_Web_App.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class ProfileViewModelTests
    {
        private List<ValidationResult> ValidateModel(ProfileViewModel profileViewModel)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(profileViewModel, null, null);
            Validator.TryValidateObject(profileViewModel, validationContext, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void Username_ShouldBeRequired()
        {
            // Arrange
            var profileViewModel = new ProfileViewModel
            {
                Username = string.Empty,
                Email = "test@example.com"
            };

            // Act
            var validationResults = ValidateModel(profileViewModel);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The Username field is required.");
        }

        [Fact]
        public void Email_ShouldBeRequired()
        {
            // Arrange
            var profileViewModel = new ProfileViewModel
            {
                Username = "testuser",
                Email = string.Empty
            };

            // Act
            var validationResults = ValidateModel(profileViewModel);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The Email field is required.");
        }

        [Fact]
        public void Email_ShouldHaveValidFormat()
        {
            // Arrange
            var profileViewModel = new ProfileViewModel
            {
                Username = "testuser",
                Email = "invalid-email-format"
            };

            // Act
            var validationResults = ValidateModel(profileViewModel);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The Email field is not a valid e-mail address.");
        }

        [Fact]
        public void ProfileViewModel_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var profileViewModel = new ProfileViewModel
            {
                Username = "testuser",
                Email = "test@example.com"
            };

            // Act
            var validationResults = ValidateModel(profileViewModel);

            // Assert
            Assert.Empty(validationResults); // Should not have validation errors
        }
    }
}
