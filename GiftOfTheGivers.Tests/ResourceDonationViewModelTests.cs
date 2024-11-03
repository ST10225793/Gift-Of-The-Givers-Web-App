using Gift_Of_The_Givers_Web_App.Models;
using Gift_Of_The_Givers_Web_App.ViewModels;
using System.Collections.Generic;
using Xunit;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class ResourceDonationViewModelTests
    {
        [Fact]
        public void ResourceDonationViewModel_CanBeInstantiated()
        {
            // Arrange
            var viewModel = new ResourceDonationViewModel();

            // Act & Assert
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void ResourceDonationViewModel_ShouldSetProperties()
        {
            // Arrange
            var viewModel = new ResourceDonationViewModel
            {
                AvailableResources = new List<Resource>
                {
                    new Resource { ResourceID = 1, Name = "Food Supplies", Description = "Non-perishable food items", Quantity = 50 },
                    new Resource { ResourceID = 2, Name = "Clothing", Description = "Winter clothing for distribution", Quantity = 30 }
                },
                SelectedResourceId = 1,
                DonorName = "John Doe",
                DonorContact = "john.doe@example.com",
                DonationMessage = "Happy to help!"
            };

            // Act & Assert
            Assert.NotNull(viewModel.AvailableResources);
            Assert.Equal(2, viewModel.AvailableResources.Count);
            Assert.Equal(1, viewModel.SelectedResourceId);
            Assert.Equal("John Doe", viewModel.DonorName);
            Assert.Equal("john.doe@example.com", viewModel.DonorContact);
            Assert.Equal("Happy to help!", viewModel.DonationMessage);
        }
    }
}
