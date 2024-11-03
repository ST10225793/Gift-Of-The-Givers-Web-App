using System.Threading.Tasks;
using Gift_Of_The_Givers_Web_App.Models;

namespace Gift_Of_The_Givers_Web_App.Services
{
    public interface IDonationService
    {
        Task AddDonationAsync(Donation donation);
    }
}
