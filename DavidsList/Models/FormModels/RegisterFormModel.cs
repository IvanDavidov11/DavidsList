namespace DavidsList.Models.FormModels
{
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using static DavidsList.Data.DataConstants;
    public class RegisterFormModel
    {
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength, ErrorMessage = "Username not in the correct range. Please try again")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "This field is required!")]
        [EmailAddress(ErrorMessage = "This is not a valid E-Mail!")]

        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = "Password not in the correct range. Please try again...")]

        public string Password { get; set; }
        
        [Required(ErrorMessage = "This field is required!")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

    }
}
