namespace Gift_Of_The_Givers_Web_App.Models
{
    public class Donation
    {
        public int DonationID { get; set; } //Primary Key
        public int? UserID { get; set; }  // Foreign Key to User
        public string Item { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }  // e.g. Pending, Completed

        public User Users { get; set; }  // Navigation property
        public List<string> AvailableResources { get; set; } = new List<string>();
    }
}
