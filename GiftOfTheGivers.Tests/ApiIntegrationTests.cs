using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using Gift_Of_The_Givers_Web_App.Data;
using Gift_Of_The_Givers_Web_App.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace GiftOfTheGivers.Tests
{
    public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_Volunteers_ReturnsSuccessStatusCode()
        {
            // Arrange
            var requestUrl = "/api/Volunteer"; // Update with your actual API endpoint

            // Act
            var response = await _client.GetAsync(requestUrl);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var volunteers = JsonConvert.DeserializeObject<List<Volunteer>>(content);
            volunteers.Should().NotBeNull();
            volunteers.Should().HaveCountGreaterThan(0); // Ensure you have data in your test database
        }

        [Fact]
        public async Task Post_Volunteer_CreatesVolunteer()
        {
            // Arrange
            var newVolunteer = new Volunteer { Name = "New Volunteer" }; // Create a new volunteer object
            var json = JsonConvert.SerializeObject(newVolunteer);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/volunteers", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var location = response.Headers.Location;
            location.Should().NotBeNull();
        }

        [Fact]
        public async Task Put_Volunteer_UpdatesVolunteer()
        {
            // Arrange
            var volunteerToUpdate = new Volunteer { VolunteerID = 1, Name = "Updated Volunteer Name" }; // Ensure this ID exists in your test data
            var json = JsonConvert.SerializeObject(volunteerToUpdate);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/api/Volunteer/1", content); // Ensure the correct endpoint

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_Volunteer_RemovesVolunteer()
        {
            // Arrange
            var volunteerId = 1; // Ensure this ID exists in your test data

            // Act
            var response = await _client.DeleteAsync($"/api/volunteers/{volunteerId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Get_NonExistentVolunteer_ReturnsNotFound()
        {
            // Arrange
            var nonExistentId = 999; // Use an ID that doesn't exist

            // Act
            var response = await _client.GetAsync($"/api/Volunteer/{nonExistentId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
