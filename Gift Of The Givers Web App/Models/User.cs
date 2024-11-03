using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Gift_Of_The_Givers_Web_App.Models
{
    public class User : IdentityUser
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username must be less than 50 characters.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a role.")]
        public string Role { get; set; } = string.Empty;

        // This property will be used for the raw password input during registration
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
