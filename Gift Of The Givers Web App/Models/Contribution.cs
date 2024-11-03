namespace Gift_Of_The_Givers_Web_App.Models
{
    public class Contribution
    {
        public int ContributionID { get; set; } // Primary Key
        public int VolunteerID { get; set; } // Foreign Key to Volunteer
        public int TaskID { get; set; } // Foreign Key to Task
        public string Status { get; set; } // e.g., Pending, Completed
        public DateTime Date { get; set; }

        // Navigation properties if necessary
        public VolunteerRegistration VolunteerRegistration { get; set; }
        public VolunteerTask Task { get; set; }
    }
}
