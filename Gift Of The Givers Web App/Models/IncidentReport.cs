using System.ComponentModel.DataAnnotations;

namespace Gift_Of_The_Givers_Web_App.Models
{
    public class IncidentReport
    {
        public int UserID { get; set; }// This can be populated from the logged-in user

        [Key]
        public int ReliefProjectID { get; set; } // Primary Key
        public string Location { get; set; }
        public string Description { get; set; }
        public string Severity { get; set; }
        public DateTime IncidentDate { get; set; }
    }

    public class IncidentReportViewModel
    {
        public string Location { get; set; }
        public string Description { get; set; }
        public string Severity { get; set; }
        public DateTime IncidentDate { get; set; }
        public int UserID { get; set; } 
    }
}
