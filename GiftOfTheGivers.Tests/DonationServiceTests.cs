using Gift_Of_The_Givers_Web_App.Data;
using Gift_Of_The_Givers_Web_App.Models;
using Gift_Of_The_Givers_Web_App.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class DonationServiceTests
    {
        private readonly DbContextOptions<GiftOfTheGiversContext> _options;

        public DonationServiceTests()
        {
            // Set up the in-memory database options for testing
            _options = new DbContextOptionsBuilder<GiftOfTheGiversContext>()
                .UseInMemoryDatabase(databaseName: "DonationTestDatabase")
                .Options;
        }

        private GiftOfTheGiversContext CreateContext()
        {
            var context = new GiftOfTheGiversContext(_options);
            context.Database.EnsureDeleted(); // Ensure a clean state
            context.Database.EnsureCreated(); // Create the database schema
            return context;
        }

        [Fact]
        public async Task AddDonationAsync_ShouldAddDonation_WhenDonationIsValid()
        {
            // Arrange
            using (var context = CreateContext())
            {
                var donationService = new DonationService(context);
                var donation = new Donation
                {
                    DonationID = 1,
                    Quantity = 100,
                    Item = "Test Donation",
                    UserID = 3,
                    Status = "Pending"
                };

                // Act
                await donationService.AddDonationAsync(donation);

                // Assert
                var result = await context.Donation.FindAsync(donation.DonationID);
                Assert.NotNull(result);
                Assert.Equal(donation.Quantity, result.Quantity);
                Assert.Equal(donation.Item, result.Item);
                Assert.Equal(donation.UserID, result.UserID);
                Assert.Equal(donation.Status, result.Status);
            }
        }

        [Fact]
        public async Task AddDonationAsync_ShouldPersistDataToDatabase()
        {
            // Arrange
            using (var context = CreateContext())
            {
                var donationService = new DonationService(context);
                var donation = new Donation
                {
                    DonationID = 2,
                    Quantity = 200,
                    Item = "Another Test Donation",
                    UserID = 3,
                    Status = "Completed"
                };

                // Act
                await donationService.AddDonationAsync(donation);

                // Assert
                var donations = await context.Donation.ToListAsync();
                Assert.Single(donations); // Ensure only one donation is in the context
                Assert.Equal(donation.Quantity, donations[0].Quantity);
                Assert.Equal(donation.Item, donations[0].Item);
                Assert.Equal(donation.UserID, donations[0].UserID);
                Assert.Equal(donation.Status, donations[0].Status);
            }

        }
    }
}
