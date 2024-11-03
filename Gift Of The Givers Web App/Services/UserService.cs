using System.Security.Claims;
using System.Threading.Tasks;
using Gift_Of_The_Givers_Web_App.Data;
using Gift_Of_The_Givers_Web_App.Models;

namespace Gift_Of_The_Givers_Web_App.Services
{
    public class UserService : IUserService
    {
        private readonly GiftOfTheGiversContext _context;

        public UserService(GiftOfTheGiversContext context)
        {
            _context = context;
        }

        public async Task<User> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _context.Users.FindAsync(userId);
        }
    }
}
