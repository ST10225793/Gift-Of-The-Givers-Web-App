using Gift_Of_The_Givers_Web_App.Data;
using Gift_Of_The_Givers_Web_App.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Gift_Of_The_Givers_Web_App.Services
{
    public class DonationService : IDonationService
    {
        private readonly GiftOfTheGiversContext _context;

        public DonationService(GiftOfTheGiversContext context)
        {
            _context = context;
        }

        public async Task AddDonationAsync(Donation donation)
        {
            _context.Donation.Add(donation);
            await _context.SaveChangesAsync(); // Save changes to the database
        }
    }
}
