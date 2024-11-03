using System.ComponentModel.DataAnnotations;

namespace Gift_Of_The_Givers_Web_App.Models
{
    public class UserContribution
    {
        [Key]
        public int ContributionID { get; set; }  // Primary Key
        public string UserID { get; set; }  // Foreign Key to User
        public string TaskName { get; set; }
        public DateTime Date { get; set; }
        public int HoursContributed { get; set; }
        public string Status { get; set; }

        // Navigation Properties
        public User User { get; set; }
    }
}
