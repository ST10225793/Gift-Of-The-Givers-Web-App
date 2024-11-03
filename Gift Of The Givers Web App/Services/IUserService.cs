using Gift_Of_The_Givers_Web_App.Models;
using System.Security.Claims;

namespace Gift_Of_The_Givers_Web_App.Services
{
    public interface IUserService
    {
        Task<User> GetCurrentUserAsync(ClaimsPrincipal user);
    }
}
