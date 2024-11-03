using Gift_Of_The_Givers_Web_App.Data;
using Gift_Of_The_Givers_Web_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class IntegrationTestBase : IAsyncDisposable
{
    protected readonly GiftOfTheGiversContext _dbContext;
    protected readonly IConfiguration _configuration;

    public IntegrationTestBase()
    {
        // Build configuration
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.json") // Add appsettings.json file
            .Build();

        var options = new DbContextOptionsBuilder<GiftOfTheGiversContext>()
            .UseSqlServer(_configuration.GetConnectionString("GiftOfTheGiversContext"))
            .Options;

        _dbContext = new GiftOfTheGiversContext(options);
        SeedTestData().Wait();
    }

    private async Task SeedTestData()
    {
        // Check if test data already exists
        if (!_dbContext.Volunteers.Any())
        {
            // Add test data here
            _dbContext.Volunteers.AddRange(new List<Volunteer>
            {
                new Volunteer { VolunteerID = 1, Name = "Test Volunteer 1" },
                new Volunteer { VolunteerID = 2, Name = "Test Volunteer 2" }
            });
            await _dbContext.SaveChangesAsync();
        }
    }

    public async ValueTask DisposeAsync()
    {
        // Cleanup code, e.g., deleting test records or resetting database
        await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM Volunteers");
        await _dbContext.DisposeAsync();
    }
}
