using System;
using Xunit;
using Gift_Of_The_Givers_Web_App.Models; // Update this namespace based on your project structure

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class IncidentReportTests
    {
        [Fact]
        public void IncidentReport_ShouldHaveRequiredFields()
        {
            // Arrange
            var incidentReport = new IncidentReport();

            // Act
            incidentReport.ReliefProjectID = 1;
            incidentReport.Description = "Test Description";
            incidentReport.IncidentDate = DateTime.Now;

            // Assert
            Assert.NotNull(incidentReport.ReliefProjectID);
            Assert.NotNull(incidentReport.Description);
            Assert.True(incidentReport.IncidentDate <= DateTime.Now, "Date should not be in the future.");
        }

        [Fact]
        public void IncidentReport_Date_ShouldNotBeInFuture()
        {
            // Arrange
            var incidentReport = new IncidentReport
            {
                ReliefProjectID = 1,
                Description = "This incident is in the future.",
                IncidentDate = DateTime.Now.AddDays(1) // Setting date in the future
            };

            // Act
            var isValid = incidentReport.IncidentDate <= DateTime.Now;

            // Assert
            Assert.False(isValid, "Incident date should not be in the future.");
        }

        [Fact]
        public void IncidentReport_Status_ShouldBeValid()
        {
            // Arrange
            var validStatuses = new[] { "High", "low", "Medium", "Closed" };
            var incidentReport = new IncidentReport
            {
                ReliefProjectID = 3,
                Description = "Testing status validation",
                Severity = "High"
            };

            // Act
            var isValidStatus = Array.Exists(validStatuses, status => status == incidentReport.Severity);

            // Assert
            Assert.True(isValidStatus, "Status should be one of the valid statuses.");
        }

        [Fact]
        public void IncidentReport_ShouldHaveDefaultValues()
        {
            // Arrange & Act
            var incidentReport = new IncidentReport
            {
                ReliefProjectID = 0, // or some other default you want to use
                Description = string.Empty,
                Severity = "Open"
            };

            // Assert
            Assert.NotNull(incidentReport.ReliefProjectID);
            Assert.NotNull(incidentReport.Description);
            Assert.Equal("Open", incidentReport.Severity); // Assuming 'Open' is the default status
        }

    }
}
