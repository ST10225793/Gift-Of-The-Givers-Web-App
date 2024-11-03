using System.ComponentModel.DataAnnotations;

namespace Gift_Of_The_Givers_Web_App.Models
{
    public class VolunteerTaskAssignment
    {
        [Key]
        public int AssignmentID { get; set; }  // Primary Key

        [Required(ErrorMessage = "The UserID field is required.")]
        public string UserID { get; set; }  // Foreign Key to User

        [Required(ErrorMessage = "The TaskID field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The TaskID must be greater than 0.")]
        public int TaskID { get; set; }  // Foreign Key to Task

        [Required(ErrorMessage = "The AssignedDate field is required.")]
        [CustomDateValidation] // Apply the custom validation attribute here
        public DateTime AssignedDate { get; set; } = DateTime.MinValue;

        public VolunteerTask Task { get; set; }  // Navigation property

        public class CustomDateValidation : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is DateTime dateTime && dateTime == DateTime.MinValue)
                {
                    return new ValidationResult("The AssignedDate must be a valid date.");
                }

                return ValidationResult.Success;
            }
        }
    }
}
