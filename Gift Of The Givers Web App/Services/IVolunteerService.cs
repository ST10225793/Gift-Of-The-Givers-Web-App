using Gift_Of_The_Givers_Web_App.Models;

namespace Gift_Of_The_Givers_Web_App.Services
{
    public interface IVolunteerService
    {
        Task<List<VolunteerTask>> GetAvailableTasksAsync();  // Retrieves available volunteer tasks
        Task<List<UserContribution>> GetUserContributionsAsync(string userId);  // Retrieves contributions for a specific user
        Task SignUpForTaskAsync(string userId, int taskId);  // Allows a user to sign up for a task
        Task RegisterVolunteerAsync(VolunteerRegistration model);  // Handles volunteer registration
        Task<List<Resource>> GetAvailableResourcesAsync();
    }
}
