using Gift_Of_The_Givers_Web_App.Data;
using Gift_Of_The_Givers_Web_App.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gift_Of_The_Givers_Web_App.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly GiftOfTheGiversContext _context;

        public VolunteerService(GiftOfTheGiversContext context)
        {
            _context = context;
        }

        public async Task<List<VolunteerTask>> GetAvailableTasksAsync()
        {
            return await _context.VolunteerTasks
                .Where(t => t.Status == "Available")
                .ToListAsync();
        }

        public async Task<List<UserContribution>> GetUserContributionsAsync(string userId)
        {
            return await _context.UserContributions
                .Where(c => c.UserID == userId)
                .ToListAsync();
        }

        public async Task SignUpForTaskAsync(string userId, int taskId)
        {
            var volunteerTask = await _context.VolunteerTasks.FindAsync(taskId);

            if (volunteerTask != null)
            {
                var assignment = new VolunteerTaskAssignment
                {
                    UserID = userId,
                    TaskID = taskId,
                    AssignedDate = DateTime.Now
                };

                _context.VolunteerTaskAssignments.Add(assignment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RegisterVolunteerAsync(VolunteerRegistration model)
        {
            var volunteer = new Volunteer
            {
                UserID = model.UserID,
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Skills = model.Skills
            };

            _context.Volunteers.Add(volunteer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Resource>> GetAvailableResourcesAsync()
        {
            return await _context.Resources.ToListAsync(); // Assuming you have a DbSet<Resource>
        }

    }
}
