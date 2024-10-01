using Gift_Of_The_Givers_Web_App.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gift_Of_The_Givers_Web_App.Data
{
    public class GiftOfTheGiversContext : IdentityDbContext<User>
    {
        public GiftOfTheGiversContext(DbContextOptions<GiftOfTheGiversContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
    }
}
