using System.Collections.Generic;
using Gift_Of_The_Givers_Web_App.Models;

namespace Gift_Of_The_Givers_Web_App.ViewModels
{
    public class ResourceDonationViewModel
    {
        // List of available resources for donation
        public List<Resource> AvailableResources { get; set; }

        // Property to hold the selected resource (optional, if you're allowing selection)
        public int SelectedResourceId { get; set; }

        // Additional properties for donation information (optional)
        public string DonorName { get; set; }
        public string DonorContact { get; set; }
        public string DonationMessage { get; set; }

        // Optionally, you could add more properties based on your form requirements
    }
}
