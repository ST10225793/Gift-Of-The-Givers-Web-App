using Gift_Of_The_Givers_Web_App.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gift_Of_The_Givers_Web_App.Data
{
    public class GiftOfTheGiversContext : IdentityDbContext<User>
    {
        public GiftOfTheGiversContext(DbContextOptions<GiftOfTheGiversContext> options) : base(options) { }

       
        public DbSet<IncidentReport> IncidentReport { get; set; }
        public DbSet<Donation> Donation { get; set; }
        public DbSet<ReliefProject> ReliefProject { get; set; }
        public DbSet<VolunteerRegistration> VolunteerRegistration { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<VolunteerTask> VolunteerTasks { get; set; }
        public DbSet<UserContribution> UserContributions { get; set; }
        public DbSet<VolunteerTaskAssignment> VolunteerTaskAssignments { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important: Call the base method

            modelBuilder.Entity<Task>().HasNoKey();

            modelBuilder.Entity<IncidentReport>()
                .HasKey(ir => ir.ReliefProjectID);

            
        }

        // Add a parameterless constructor
        public GiftOfTheGiversContext() : base(new DbContextOptions<GiftOfTheGiversContext>())
        {
        }

    }
}
