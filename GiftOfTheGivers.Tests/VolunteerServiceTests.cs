using Gift_Of_The_Givers_Web_App.Data;
using Gift_Of_The_Givers_Web_App.Models;
using Gift_Of_The_Givers_Web_App.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Gift_Of_The_Givers_Web_App.Tests
{
    public class VolunteerServiceTests
    {
        private readonly DbContextOptions<GiftOfTheGiversContext> _options;

        public VolunteerServiceTests()
        {
            // Set up the in-memory database options for testing
            _options = new DbContextOptionsBuilder<GiftOfTheGiversContext>()
                .UseInMemoryDatabase(databaseName: "VolunteerTestDatabase")
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
        public async Task GetAvailableTasksAsync_ShouldReturnAvailableTasks()
        {
            // Arrange
            using (var context = CreateContext())
            {
                context.VolunteerTasks.AddRange(new List<VolunteerTask>
            {
                new VolunteerTask { TaskID = 1, Status = "Available", Description = "To do", Name = "Clean Up" },
                new VolunteerTask { TaskID = 2, Status = "Unavailable", Name = "Laundry", Description = "Do laundry for the elderly" }
            });
                await context.SaveChangesAsync();

                var volunteerService = new VolunteerService(context);

                // Act
                var result = await volunteerService.GetAvailableTasksAsync();

                // Assert
                Assert.Single(result);
                Assert.Equal(1, result.First().TaskID);
            }
        }

        [Fact]
        public async Task GetUserContributionsAsync_ShouldReturnUserContributions()
        {
            // Arrange
            var userId = "user1";
            using (var context = CreateContext())
            {
                context.UserContributions.AddRange(new List<UserContribution>
        {
            new UserContribution { ContributionID = 1, UserID = userId, TaskName = "Task 1", Date = DateTime.Now, HoursContributed = 5, Status = "Completed" },
            new UserContribution { ContributionID = 2, UserID = userId, TaskName = "Task 2", Date = DateTime.Now, HoursContributed = 3, Status = "Pending" },
            new UserContribution { ContributionID = 3, UserID = "user2", TaskName = "Task 3", Date = DateTime.Now, HoursContributed = 2, Status = "Completed" }
        });
                await context.SaveChangesAsync();

                var volunteerService = new VolunteerService(context);

                // Act
                var result = await volunteerService.GetUserContributionsAsync(userId);

                // Assert
                Assert.Equal(2, result.Count); // Expecting 2 contributions for user1
                Assert.All(result, c => Assert.Equal(userId, c.UserID)); // All contributions should belong to user1
            }
        }

        [Fact]
        public async Task SignUpForTaskAsync_ShouldAddAssignment_WhenTaskExists()
        {
            // Arrange
            var userId = "user123";
            var taskId = 1;
            using (var context = CreateContext())
            {
                context.VolunteerTasks.Add(new VolunteerTask { TaskID = taskId, Status = "Available", Description = "Marathon", Name = "Walk A Mile" });
                await context.SaveChangesAsync();

                var volunteerService = new VolunteerService(context);

                // Act
                await volunteerService.SignUpForTaskAsync(userId, taskId);

                // Assert
                var assignments = await context.VolunteerTaskAssignments.ToListAsync();
                Assert.Single(assignments);
                Assert.Equal(userId, assignments[0].UserID);
                Assert.Equal(taskId, assignments[0].TaskID);
            }
        }

        [Fact]
        public async Task SignUpForTaskAsync_ShouldNotAddAssignment_WhenTaskDoesNotExist()
        {
            // Arrange
            var userId = "user123";
            var taskId = 999; // Non-existing task
            using (var context = CreateContext())
            {
                var volunteerService = new VolunteerService(context);

                // Act
                await volunteerService.SignUpForTaskAsync(userId, taskId);

                // Assert
                var assignments = await context.VolunteerTaskAssignments.ToListAsync();
                Assert.Empty(assignments); // No assignments should be added
            }
        }

        [Fact]
        public async Task RegisterVolunteerAsync_ShouldAddVolunteer()
        {
            // Arrange
            var model = new VolunteerRegistration
            {
                UserID = 1,
                Name = "Test Volunteer",
                Email = "test@example.com",
                Phone = "1234567890",
                Skills = "First Aid"
            };

            using (var context = CreateContext())
            {
                var volunteerService = new VolunteerService(context);

                // Act
                await volunteerService.RegisterVolunteerAsync(model);

                // Assert
                var volunteers = await context.Volunteers.ToListAsync();
                Assert.Single(volunteers);
                Assert.Equal(model.UserID, volunteers[0].UserID);
                Assert.Equal(model.Name, volunteers[0].Name);
            }
        }

        [Fact]
        public async Task GetAvailableResourcesAsync_ShouldReturnAllResources()
        {
            // Arrange
            using (var context = CreateContext())
            {
                context.Resources.AddRange(new List<Resource>
            {
                new Resource { ResourceID = 1, Name = "Water", Description = "Water bottles", Quantity = 12 },
                new Resource { ResourceID = 2, Name = "Food", Description = "Naan bread", Quantity = 12 }
            });
                await context.SaveChangesAsync();

                var volunteerService = new VolunteerService(context);

                // Act
                var result = await volunteerService.GetAvailableResourcesAsync();

                // Assert
                Assert.Equal(2, result.Count);
            }
        }
    }
}
