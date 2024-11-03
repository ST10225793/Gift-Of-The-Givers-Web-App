namespace Gift_Of_The_Givers_Web_App.Models
{
    public class Volunteer
    {
        public int VolunteerID { get; set; } // Primary Key
        public int UserID { get; set; } //Foreign Key
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Skills { get; set; }

        // Navigation Properties
        public User User { get; set; }
    }
}
