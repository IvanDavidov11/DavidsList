namespace DavidsList.Models.FormModels
{
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using static DavidsList.Data.DataConstants;
    public class RegisterFormModel
    {
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(UsernameMaxLength, ErrorMessage ="Username not in the correct range. Please try again", MinimumLength = UsernameMinLength)]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "This field is required!")]
        [EmailAddress(ErrorMessage = "This is not a valid E-Mail!")]

        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [StringLength(PasswordMaxLength, ErrorMessage = "Password not in the correct range. Please try again...", MinimumLength = PasswordMinLength)]

        public string Password { get; set; }
        
        [Required(ErrorMessage = "This field is required!")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

    }
}
