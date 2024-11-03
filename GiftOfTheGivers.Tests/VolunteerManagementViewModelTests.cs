using Gift_Of_The_Givers_Web_App.Models;
using System.Collections.Generic;
using Xunit;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class VolunteerManagementViewModelTests
    {
        [Fact]
        public void VolunteerManagementViewModel_CanBeInstantiated()
        {
            // Arrange & Act
            var viewModel = new VolunteerManagementViewModel();

            // Assert
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void VolunteerManagementViewModel_ShouldSetAvailableTasks()
        {
            // Arrange
            var viewModel = new VolunteerManagementViewModel();
            var tasks = new List<VolunteerTask>
            {
                new VolunteerTask { TaskID = 1, Name = "Clean Up" },
                new VolunteerTask { TaskID = 2, Name = "Food Distribution" }
            };

            // Act
            viewModel.AvailableTasks = tasks;

            // Assert
            Assert.NotNull(viewModel.AvailableTasks);
            Assert.Equal(2, viewModel.AvailableTasks.Count);
            Assert.Equal("Clean Up", viewModel.AvailableTasks[0].Name);
            Assert.Equal("Food Distribution", viewModel.AvailableTasks[1].Name);
        }

        [Fact]
        public void VolunteerManagementViewModel_ShouldSetUserContributions()
        {
            // Arrange
            var viewModel = new VolunteerManagementViewModel();
            var contributions = new List<UserContribution>
            {
                new UserContribution { ContributionID = 1, TaskName = "Clean Up", HoursContributed = 3 },
                new UserContribution { ContributionID = 2, TaskName = "Food Distribution", HoursContributed = 5 }
            };

            // Act
            viewModel.UserContributions = contributions;

            // Assert
            Assert.NotNull(viewModel.UserContributions);
            Assert.Equal(2, viewModel.UserContributions.Count);
            Assert.Equal(1, viewModel.UserContributions[0].ContributionID);
            Assert.Equal("Clean Up", viewModel.UserContributions[0].TaskName);
            Assert.Equal(3, viewModel.UserContributions[0].HoursContributed);
        }
    }
}
