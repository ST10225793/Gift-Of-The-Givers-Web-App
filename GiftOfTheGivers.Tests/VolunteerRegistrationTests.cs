using Gift_Of_The_Givers_Web_App.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class VolunteerRegistrationTests
    {
        [Fact]
        public void VolunteerRegistration_CanBeInstantiated()
        {
            // Arrange & Act
            var registration = new VolunteerRegistration();

            // Assert
            Assert.NotNull(registration);
        }

        [Fact]
        public void VolunteerRegistration_ShouldSetName()
        {
            // Arrange
            var registration = new VolunteerRegistration();
            var name = "Marry Poppin";

            // Act
            registration.Name = name;

            // Assert
            Assert.Equal(name, registration.Name);
        }

        [Fact]
        public void VolunteerRegistration_ShouldSetEmail()
        {
            // Arrange
            var registration = new VolunteerRegistration();
            var email = "marryp@example.com";

            // Act
            registration.Email = email;

            // Assert
            Assert.Equal(email, registration.Email);
        }

        [Fact]
        public void VolunteerRegistration_ShouldSetPhone()
        {
            // Arrange
            var registration = new VolunteerRegistration();
            var phone = "0764569870";

            // Act
            registration.Phone = phone;

            // Assert
            Assert.Equal(phone, registration.Phone);
        }

        [Fact]
        public void VolunteerRegistration_ShouldSetSkills()
        {
            // Arrange
            var registration = new VolunteerRegistration();
            var skills = "First Aid, CPR";

            // Act
            registration.Skills = skills;

            // Assert
            Assert.Equal(skills, registration.Skills);
        }

        [Fact]
        public void VolunteerRegistration_Validation_ShouldFail_WhenEmailIsNull()
        {
            // Arrange
            var registration = new VolunteerRegistration
            {
                Name = "Marry Poppin",
                Phone = "0123456789",
                Skills = "First Aid, CPR",
                Email = null // Invalid email
            };

            // Act
            var context = new ValidationContext(registration);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(registration, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Email is required."); // Update expected message
        }

        [Fact]
        public void VolunteerRegistration_Validation_ShouldFail_WhenEmailIsInvalid()
        {
            // Arrange
            var registration = new VolunteerRegistration
            {
                Name = "Marry Poppin",
                Phone = "0123456789",
                Skills = "First Aid, CPR",
                Email = "not-an-email" // Invalid email format
            };

            // Act
            var context = new ValidationContext(registration);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(registration, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Invalid email address format."); // Update expected message
        }

    }
}
