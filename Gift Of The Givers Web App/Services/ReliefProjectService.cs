using Gift_Of_The_Givers_Web_App.Models;
using Gift_Of_The_Givers_Web_App.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gift_Of_The_Givers_Web_App.Services
{
    public class ReliefProjectService : IReliefProjectService

    {
        private readonly GiftOfTheGiversContext _context;

        public ReliefProjectService(GiftOfTheGiversContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReliefProject>> GetAvailableProjectsAsync()
        {
            return await _context.ReliefProject.ToListAsync();
        }
    }
}
